﻿using System;
using System.Web;
using OfficeOpenXml;
using System.Data;
using System.Linq;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TestPOSTWebService
{
    public static class ExcelPackageExtensions
    {
        public static DataTable ToDataTable(this ExcelPackage package)
        {
            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();

            Dictionary<string, bool> dictCols = new Dictionary<string, bool>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CommentsConnectionString"].ConnectionString))
            {
                conn.Open();

                var schema = conn.GetSchema("Columns", new[] { null, null, "Initial", null });
                foreach (DataRow row in schema.Rows)
                {
                    String sCol = (string)row["COLUMN_NAME"];

                    if (!sCol.Equals("numRow")
                        && !sCol.Equals("decVariance"))
                    {
                        dictCols.Add(sCol, true);

                        var sExamine1 = sCol.Substring(0, 3);

                        if (sCol.Substring(0, 3).Equals("dec"))
                        {
                            table.Columns.Add(sCol, typeof(decimal));

                        }
                        else if (sCol.Substring(0, 3).Equals("dat"))
                        {
                            table.Columns.Add(sCol, typeof(DateTime));
                        }
                        else
                        {
                            table.Columns.Add(sCol);
                        }
                    }
                }

                conn.Close();
            }

            // go through all rows
            for (int rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                var newRow = table.NewRow();

                // add in missing columns to complete data-set
                // sproc is expecting entire schema for upsert
                for (int i = 0; i < dictCols.Count; i++)
                {
                    string dx="", px="";
                    newRow[i] = DBNull.Value;

                    // source columns could be in any order :-\
                    for (int j = 1; j <= workSheet.Dimension.End.Column; j++)
                    {
                        if (dictCols.Keys.ElementAt(i)
                            .Equals(workSheet.Cells[1, j].Value.ToString()))
                        {
                            newRow[i] = workSheet.Cells[rowNumber, j].Text;
                            break;
                        }

                       // added by rb
                        if (workSheet.Cells[1, j].Text.Contains("DX") && dictCols.Keys.ElementAt(i).Equals("vcDX"))
                        {
                            dx = dx + workSheet.Cells[rowNumber, j].Text + "|";
                            newRow[i] = dx;
                        }

                        // added by rb
                        if (workSheet.Cells[1, j].Text.Contains("PX") && dictCols.Keys.ElementAt(i).Equals("vcPX"))
                        {
                            px = px + workSheet.Cells[rowNumber, j].Text + "|";
                            newRow[i] = px;
                        }

                    }
                }

                table.Rows.Add(newRow);
            }
            return table;
        }

        public static DataTable ToFollowUpDataTable(this ExcelPackage package)
        {
            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();

            Dictionary<string, bool> dictCols = new Dictionary<string, bool>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CommentsConnectionString"].ConnectionString))
            {
                conn.Open();

                var schema = conn.GetSchema("Columns", new[] { null, null, "FollowUp", null });
                foreach (DataRow row in schema.Rows)
                {
                    String sCol = (string)row["COLUMN_NAME"];

                    if (!sCol.Equals("numRowFu"))
                    {
                        dictCols.Add(sCol, true);

                        var sExamine1 = sCol.Substring(0, 3);

                        if (sCol.Substring(0, 3).Equals("dec"))
                        {
                            table.Columns.Add(sCol, typeof(decimal));

                        }
                        else if (sCol.Substring(0, 3).Equals("dat"))
                        {
                            table.Columns.Add(sCol, typeof(DateTime));
                        }
                        else
                        {
                            table.Columns.Add(sCol);
                        }
                    }
                }

                conn.Close();
            }

            String sBuf, sDate, sBy, sComment;
            String[] sxFollowUpFields;
            StringBuilder sbValues = new StringBuilder();

            int idxVcAcctNo = 0;
            int idxVcClient = 0;

            // find key fields for merge
            for (int j = 1; j <= workSheet.Dimension.End.Column; j++)
            {
                sBuf = workSheet.Cells[1, j].Text;

                if (sBuf.Length >= 8)
                {
                    if (sBuf.Substring(0, 8).Equals("vcAcctNo"))
                    {
                        idxVcAcctNo = j;
                    }
                    else if (sBuf.Substring(0, 8).Equals("vcClient"))
                    {
                        idxVcClient = j;
                    }
                }
            }

            // find any follow-up comment fields
            for (int j = 1; j <= workSheet.Dimension.End.Column; j++)
            {
                sBuf = workSheet.Cells[1, j].Text;

                if (sBuf.Length >= 12)
                {
                    if (sBuf.Substring(0, 12).ToUpper().Equals("ADD COMMENT,"))
                    {

                        sDate = "";
                        sBy = "";

                        sxFollowUpFields = sBuf.Substring(12).Split(',');
                        foreach (String s in sxFollowUpFields)
                        {
                            if (s.Trim().Substring(0, 5).ToUpper().Equals("DATE:"))
                                sDate = s.Trim().Substring(5).Trim();
                            else if (s.Trim().Substring(0, 3).ToUpper().Equals("BY:"))
                                sBy = s.Trim().Substring(3).Trim();
                        }

                        // go through all rows
                        for (int rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
                        {

                            sComment = workSheet.Cells[rowNumber, j].Text.Trim();

                            if (sComment.Length > 0)
                            {
                                var newRow = table.NewRow();

                                for (int i = 0; i < dictCols.Count; i++)
                                {
                                    newRow[i] = DBNull.Value;
                                }

                                newRow[1] = sDate;
                                newRow[3] = sBy;
                                newRow[4] = sComment;

                                // temporary values for testing merge
                                if (idxVcClient > 0)
                                    newRow[dictCols.Count - 1] = workSheet.Cells[rowNumber, idxVcClient].Text.Trim();
                                else
                                    newRow[dictCols.Count - 1] = DBNull.Value;

                                if(idxVcAcctNo > 0)
                                    newRow[dictCols.Count - 2] = workSheet.Cells[rowNumber, idxVcAcctNo].Text.Trim();
                                else
                                    newRow[dictCols.Count - 2] = DBNull.Value;
                                table.Rows.Add(newRow);
                            }
                        }
                    }
                }
            }

            return table;
        }
    }
}
