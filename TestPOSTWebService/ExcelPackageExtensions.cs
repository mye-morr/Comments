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

            Dictionary<string, bool> dictCols = new Dictionary<string, bool>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CommentsConnectionString"].ConnectionString))
            {
                conn.Open();

                var schema = conn.GetSchema("Columns", new[] { null, null, "Initial", null });
                foreach (DataRow row in schema.Rows)
                {
                    String sCol = (string)row["COLUMN_NAME"];

                    if (!sCol.Equals("numRow")
                        && !sCol.Equals("decVariance")) {
                        dictCols.Add(sCol, true);
                    }            
                }
            }

            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();
            foreach (var firstRowCell in workSheet.Cells[1, 1, 1, workSheet.Dimension.End.Column])
            {
                table.Columns.Add(firstRowCell.Text);
                dictCols.Remove(firstRowCell.Text);
            }

            // add in missing columns to complete data-set
            // sproc is expecting entire schema for upsert
            foreach(var sCol in dictCols.Keys)
            {
                table.Columns.Add(sCol);
            }

            for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                var row = workSheet.Cells[rowNumber, 1, rowNumber, workSheet.Dimension.End.Column];
                var newRow = table.NewRow();

                int iCol = 0;
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text;
                    iCol = cell.Start.Column - 1;
                }

                // fill null values for extra columns
                for(var i = 1; i < dictCols.Count; i++)
                {
                    newRow[iCol + i] = DBNull.Value;
                }

                table.Rows.Add(newRow);
            }
            return table;
        }

        /*
        private DataSet GridDataTable(string Query)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["CommentsConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                using (SqlCommand comm = new SqlCommand(Query + ";" + sSQLSelectAllAccounts() + ";" + sSQLSelectAccount() + ";" + sSQLSelectFollowUp(), conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(comm);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    return ds;
                }
            }
        }
        */
    }
}
