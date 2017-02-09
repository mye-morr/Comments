using System;
using System.Web;
using OfficeOpenXml;
using System.Data;
using System.Linq;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

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
                            table.Columns.Add(sCol,typeof(decimal));

                        }
                        else if (sCol.Substring(0, 3).Equals("dat"))
                        {
                            table.Columns.Add(sCol,typeof(DateTime));
                        }
                        else
                        {
                            table.Columns.Add(sCol);
                        }
                    }            
                }
            }

            // go through all rows
            for (int rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                var newRow = table.NewRow();

                // add in missing columns to complete data-set
                // sproc is expecting entire schema for upsert
                for (int i = 0; i < dictCols.Count; i++)
                {
                    String dx = System.String.Empty;

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

                        //added by rb
                        if (workSheet.Cells[1, j].Text.Substring(0, 2).Equals("DX") && dictCols.Keys.ElementAt(i).Equals("vcDX"))
                        {
                            dx = dx + workSheet.Cells[rowNumber, j].Text + "|";
                            newRow[i] = dx;
                            //break;
                        }
                    }

                }

                table.Rows.Add(newRow);
            }
            return table;
        }
    }
}
