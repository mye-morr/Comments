using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Globalization;
using OfficeOpenXml;
using System.IO;
using System.Drawing;

namespace TestPOSTWebService
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //Persist the table in the Session object.
                Session["AccountsTable"] = GridDataTable().Tables[0];

                //Bind the GridView control to the data source.
                GridView1.DataSource = Session["AccountsTable"];
                GridView1.DataBind();

                if (!String.IsNullOrEmpty(Request.QueryString["vcAcctNo"]))
                {
                    txtSearch.Text = Request.QueryString["vcAcctNo"];
                    viewAcct();
                }
            }
            else
            {
                String foo = this.Hidden1.Value;
                if (foo.Length > 0)
                {

                    var excel = new ExcelPackage(File1.PostedFile.InputStream);
                    var dt = excel.ToDataTable();
                    var dtf = excel.ToFollowUpDataTable();

                    using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CommentsConnectionString"].ConnectionString))
                    {

                        using (SqlCommand cmd = new SqlCommand("Upsert_Initial"))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Connection = conn;
                            cmd.Parameters.AddWithValue("@tblData", dt);
                            cmd.CommandTimeout = 0;
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }

                        using (SqlCommand cmd = new SqlCommand("Upsert_FollowUp"))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Connection = conn;
                            cmd.Parameters.AddWithValue("@tblData", dtf);
                            cmd.CommandTimeout = 0;
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                }

                // otherwise, it loads the data twice!!
                this.Hidden1.Value = "";

                // REFRESH DATA!, see !IsPostBack
                //Persist the table in the Session object.
                Session["AccountsTable"] = GridDataTable().Tables[0];

                //Bind the GridView control to the data source.
                GridView1.DataSource = Session["AccountsTable"];
                GridView1.DataBind();
            }
        }


        protected void DataList4_EditCommand(Object sender, DataListCommandEventArgs e)
        {
            DataList4.EditItemIndex = e.Item.ItemIndex;

            DataList4.DataSource = GridDataTable().Tables[2];
            DataList4.DataBind();
        }

        protected void DataList4_DeleteCommand(Object sender, DataListCommandEventArgs e)
        {
            DataList4.DataKeys[e.Item.ItemIndex].ToString();
            string numRowFu = DataList4.DataKeys[e.Item.ItemIndex].ToString();
            string Query = "Delete FollowUp WHERE numRowFu=" + numRowFu;

            DataSet ds = GridDataTable(Query);

            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();

            DataList4.DataSource = ds.Tables[2];
            DataList4.DataBind();
        }

        protected void DataList4_CancelCommand(Object sender, DataListCommandEventArgs e)
        {
            DataList4.EditItemIndex = -1;

            DataList4.DataSource = GridDataTable().Tables[2];
            DataList4.DataBind();
        }

        protected void DataList4_UpdateCommand(object sender, DataListCommandEventArgs e)
        {
            string numRow = DataList4.DataKeys[e.Item.ItemIndex].ToString();

            TextBox txtVcCallRefNo = e.Item.FindControl("txtVcCallRefNo") as TextBox;
            TextBox txtVcFuComment = e.Item.FindControl("txtVcFuComment") as TextBox;
            TextBox txtVcContactName = e.Item.FindControl("txtVcContactName") as TextBox;
            TextBox txtVcContactPhone = e.Item.FindControl("txtVcContactPhone") as TextBox;
            TextBox txtVcContactEmail = e.Item.FindControl("txtVcContactEmail") as TextBox;
            TextBox txtDatFollowUp = e.Item.FindControl("txtDatFollowUp") as TextBox;

            String UpdateQuery = string.Format(
                "UPDATE FollowUp SET "
                    + "vcCallRefNo={0},"
                    + "vcFuComment={1},"
                    + "vcContactName={2},"
                    + "vcContactPhone={3},"
                    + "vcContactEmail={4},"
                    + "datFollowUp={5} "
                + "WHERE numRowFu={6}",
                    txtVcCallRefNo.Text.Equals("") ? "NULL" : "'" + txtVcCallRefNo.Text + "'",
                    txtVcFuComment.Text.Equals("") ? "NULL" : "'" + txtVcFuComment.Text + "'",
                    txtVcContactName.Text.Equals("") ? "NULL" : "'" + txtVcContactName.Text + "'",
                    txtVcContactPhone.Text.Equals("") ? "NULL" : "'" + txtVcContactPhone.Text + "'",
                    txtVcContactEmail.Text.Equals("") ? "NULL" : "'" + txtVcContactEmail.Text + "'",
                    txtDatFollowUp.Text.Equals("") ? "NULL" : "'" + Convert.ToDateTime(txtDatFollowUp.Text) + "'",
                    Convert.ToInt32(numRow)
                );

            DataList4.EditItemIndex = -1;

            DataSet ds = GridDataTable(UpdateQuery);
            Session["AccountsTable"] = ds.Tables[0];

            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
            DataList4.DataSource = ds.Tables[2];
            DataList4.DataBind();
        }

        protected void GridView2_RowEditing(Object sender, GridViewEditEventArgs e)
        {
            GridView2.EditIndex = e.NewEditIndex;

            GridView2.DataSource = GridDataTable().Tables[1];
            GridView2.DataBind();
        }

        protected void GridView2_RowCancelling(Object sender, GridViewCancelEditEventArgs e)
        {
            GridView2.EditIndex = -1;

            GridView2.DataSource = GridDataTable().Tables[1];
            GridView2.DataBind();
        }

        protected void GridView2_RowDeleting(Object sender, GridViewDeleteEventArgs e)
        {
            string numRow = GridView2.DataKeys[e.RowIndex].Value.ToString();
            string Query = "Delete Initial WHERE Initial.numRow=" + numRow;

            DataSet ds = GridDataTable(Query);
            Session["AccountsTable"] = ds.Tables[0];

            GridView1.DataSource = Session["AccountsTable"];
            GridView1.DataBind();

            GridView2.DataSource = ds.Tables[1];
            GridView2.DataBind();
        }

        protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string numRow = GridView1.DataKeys[e.RowIndex].Value.ToString();

            TextBox txtVcAcctNo = GridView2.Rows[e.RowIndex].FindControl("txtVcAcctNo") as TextBox;
            TextBox txtVcClient = GridView2.Rows[e.RowIndex].FindControl("txtVcClient") as TextBox;
            TextBox txtVcPatName = GridView2.Rows[e.RowIndex].FindControl("txtVcPatName") as TextBox;
            TextBox txtVcPatSSN = GridView2.Rows[e.RowIndex].FindControl("txtVcPatSSN") as TextBox;
            TextBox txtVcPatIns = GridView2.Rows[e.RowIndex].FindControl("txtVcPatIns") as TextBox;
            TextBox txtVcPatInsIdNo = GridView2.Rows[e.RowIndex].FindControl("txtVcPatInsIdNo") as TextBox;
            TextBox txtDecTotalChgs = GridView2.Rows[e.RowIndex].FindControl("txtDecTotalChgs") as TextBox;
            TextBox txtDecExpected = GridView2.Rows[e.RowIndex].FindControl("txtDecExpected") as TextBox;
            TextBox txtVcUpCategory = GridView2.Rows[e.RowIndex].FindControl("txtVcUpCategory") as TextBox;

            String UpdateQuery = string.Format(
                "UPDATE Initial SET "
                    + "vcAcctNo={0},"
                    + "vcClient={1},"
                    + "vcPatName={2},"
                    + "vcPatSSN={3},"
                    + "vcPatIns={4},"
                    + "vcPatInsIdNo={5},"
                    + "decTotalChgs={6},"
                    + "decExpected={7},"
                    + "vcUpCategory={8} "
                + "WHERE numRow={9}",
                    txtVcAcctNo.Text.Equals("") ? "NULL" : "'" + txtVcAcctNo.Text + "'",
                    txtVcClient.Text.Equals("") ? "NULL" : "'" + txtVcClient.Text + "'",
                    txtVcPatName.Text.Equals("") ? "NULL" : "'" + txtVcPatName.Text + "'",
                    txtVcPatSSN.Text.Equals("") ? "NULL" : "'" + txtVcPatSSN.Text + "'",
                    txtVcPatIns.Text.Equals("") ? "NULL" : "'" + txtVcPatIns.Text + "'",
                    txtVcPatInsIdNo.Text.Equals("") ? "NULL" : "'" + txtVcPatInsIdNo.Text + "'",
                    txtDecTotalChgs.Text.Equals("") ? "NULL" : "'" + txtDecTotalChgs.Text + "'",
                    txtDecExpected.Text.Equals("") ? "NULL" : "'" + txtDecExpected.Text + "'",
                    txtVcUpCategory.Text.Equals("") ? "NULL" : "'" + txtVcUpCategory.Text + "'",
                    Convert.ToInt32(numRow)
                );

            GridView2.EditIndex = -1;

            DataSet ds = GridDataTable(UpdateQuery);
            Session["AccountsTable"] = ds.Tables[0];

            GridView1.DataSource = Session["AccountsTable"];
            GridView1.DataBind();

            GridView2.DataSource = ds.Tables[1];
            GridView2.DataBind();

            DataList2.DataSource = ds.Tables[1];
            DataList2.DataBind();
        }

        protected void GridView1_RowEditing(Object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;

            GridView1.DataSource = GridDataTable().Tables[0];
            GridView1.DataBind();
        }

        protected void GridView1_RowCancelling(Object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;

            GridView1.DataSource = GridDataTable().Tables[0];
            GridView1.DataBind();
        }

        protected void GridView1_RowDeleting(Object sender, GridViewDeleteEventArgs e)
        {
            string numRow = GridView1.DataKeys[e.RowIndex].Value.ToString();
            string Query = "Delete Initial WHERE Initial.numRow=" + numRow;

            DataSet ds = GridDataTable(Query);
            Session["AccountsTable"] = ds.Tables[0];

            GridView1.DataSource = Session["AccountsTable"];
            GridView1.DataBind();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string numRow = GridView1.DataKeys[e.RowIndex].Value.ToString();

            TextBox txtVcCommentBy = GridView1.Rows[e.RowIndex].FindControl("txtVcCommentBy") as TextBox;
            TextBox txtVcComment = GridView1.Rows[e.RowIndex].FindControl("txtVcComment") as TextBox;
            TextBox txtVcAcctNo = GridView1.Rows[e.RowIndex].FindControl("txtVcAcctNo") as TextBox;
            TextBox txtVcClient = GridView1.Rows[e.RowIndex].FindControl("txtVcClient") as TextBox;
            TextBox txtVcPatName = GridView1.Rows[e.RowIndex].FindControl("txtVcPatName") as TextBox;
            TextBox txtVcPatSSN = GridView1.Rows[e.RowIndex].FindControl("txtVcPatSSN") as TextBox;
            TextBox txtVcPatIns = GridView1.Rows[e.RowIndex].FindControl("txtVcPatIns") as TextBox;
            TextBox txtVcPatInsIdNo = GridView1.Rows[e.RowIndex].FindControl("txtVcPatInsIdNo") as TextBox;
            TextBox txtDecTotalChgs = GridView1.Rows[e.RowIndex].FindControl("txtDecTotalChgs") as TextBox;
            TextBox txtDecExpected = GridView1.Rows[e.RowIndex].FindControl("txtDecExpected") as TextBox;
            TextBox txtVcUpCategory = GridView1.Rows[e.RowIndex].FindControl("txtVcUpCategory") as TextBox;

            String UpdateQuery = string.Format(
                "UPDATE Initial SET "
                    + "vcCommentBy='{0}',"
                    + "vcComment='{1}',"
                    + "vcAcctNo={2},"
                    + "vcClient={3},"
                    + "vcPatName={4},"
                    + "vcPatSSN={5},"
                    + "vcPatIns={6},"
                    + "vcPatInsIdNo={7},"
                    + "decTotalChgs={8},"
                    + "decExpected={9},"
                    + "vcUpCategory={10} "
                + "WHERE numRow={11}",
                    txtVcCommentBy.Text,
                    txtVcComment.Text,
                    txtVcAcctNo.Text.Equals("") ? "NULL" : "'" + txtVcAcctNo.Text + "'",
                    txtVcClient.Text.Equals("") ? "NULL" : "'" + txtVcClient.Text + "'",
                    txtVcPatName.Text.Equals("") ? "NULL" : "'" + txtVcPatName.Text + "'",
                    txtVcPatSSN.Text.Equals("") ? "NULL" : "'" + txtVcPatSSN.Text + "'",
                    txtVcPatIns.Text.Equals("") ? "NULL" : "'" + txtVcPatIns.Text + "'",
                    txtVcPatInsIdNo.Text.Equals("") ? "NULL" : "'" + txtVcPatInsIdNo.Text + "'",
                    txtDecTotalChgs.Text.Equals("") ? "NULL" : "'" + txtDecTotalChgs.Text + "'",
                    txtDecExpected.Text.Equals("") ? "NULL" : "'" + txtDecExpected.Text + "'",
                    txtVcUpCategory.Text.Equals("") ? "NULL" : "'" + txtVcUpCategory.Text + "'",
                    Convert.ToInt32(numRow)
                );

            GridView1.EditIndex = -1;

            DataSet ds = GridDataTable(UpdateQuery);
            Session["AccountsTable"] = ds.Tables[0];

            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();

            if (!txtSearch.Text.Equals(""))
            {
                DataList2.DataSource = ds.Tables[1];
                DataList2.DataBind();

                GridView2.DataSource = ds.Tables[1];
                GridView2.DataBind();

            }
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {

            //Retrieve the table from the session object
            DataTable dt = Session["AccountsTable"] as DataTable;

            if (dt != null)
            {
                //Sort the data.
                dt.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }

        }

        private string GetSortDirection(string column)
        {

            // By default, set the sort direction to ascending.
            string sortDirection = "ASC";

            // Retrieve the last column that was sorted.
            string sortExpression = ViewState["SortExpression"] as string;

            if (sortExpression != null)
            {
                // Check if the same column is being sorted.
                // Otherwise, the default value can be returned.
                if (sortExpression == column)
                {
                    string lastDirection = ViewState["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }

            // Save new values in ViewState.
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;

            return sortDirection;
        }

        private DataSet GridDataTable()
        {
            return GridDataTable("");
        }

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

        protected string sSQLSelectAllAccounts()
        {
            string sSQL = "SELECT TOP 1000 * FROM (SELECT DISTINCT (STUFF((SELECT '||' + CONVERT(VARCHAR(10), datFuComment) + COALESCE(+ ', ' + vcFuCommentBy,'') +'|' + vcFuComment FROM FollowUp f WHERE f.numInitialRow = i.numRow ORDER BY datComment FOR XML PATH(''), TYPE, ROOT).value('root[1]', 'nvarchar(max)'), 1, 2, '')) as FollowUpComments, (SELECT MAX(datFollowUp) FROM FollowUp f WHERE f.numInitialRow = numRow) as FollowUpDate, i.* FROM Initial i LEFT OUTER JOIN FollowUp f ON f.numInitialRow = i.numRow) as t";

            string sSQLTail = txtCustomSQL.Text;

            if (!sSQLTail.Equals(""))
            {
                if (sSQLTail.Length >= 6)
                {
                    if (sSQLTail.Substring(0, 6).ToUpper().Equals("DELETE"))
                    {
                        // do nothing for DELETE query
                    }
                    else
                    {
                        if (sSQLTail.Length >= 8)
                        {
                            if (sSQLTail.Substring(0, 8).ToUpper().Equals("ORDER BY"))
                            {
                                sSQL += " " + sSQLTail;
                            }
                            else
                            {
                                sSQL += " WHERE " + sSQLTail;
                            }
                        }
                        else
                        {
                            sSQL += " WHERE " + sSQLTail;
                        }
                    }
                }
            }

            return sSQL;
        }

        protected string sSQLSelectAccount()
        {
            string sSQL = "SELECT * FROM Initial";

            if (!txtSearch.Text.Equals(""))
            {
                sSQL += " WHERE vcAcctNo='" + txtSearch.Text + "'";
            }

            return sSQL;
        }

        protected string sSQLSelectFollowUp()
        {
            string sSQL = "";

            if (!txtSearch.Text.Equals(""))
            {
                sSQL += "Select f.* FROM Initial i INNER JOIN FollowUp f ON i.numRow = f.numInitialRow WHERE i.vcAcctNo = '" + txtSearch.Text + "'";
            }

            return sSQL;
        }

        protected void btnAppendFollowUp_Click(object sender, EventArgs e)
        {
            if (!txtNumInitialRow.Text.Equals(""))
            {
                string[] formats = { "M/d/yyyy", "M/dd/yyyy", "MM/d/yyyy", "MM/dd/yyyy" };

                DateTime dateValue;
                if (DateTime.TryParseExact(txtDatFollowUp.Text, formats, new CultureInfo("en-US"), DateTimeStyles.None, out dateValue)
                    || txtDatFollowUp.Text.Equals(""))
                {
                    String InsertQuery = string.Format(
                       "Insert into FollowUp(numInitialRow,vcFuCommentBy,vcFuComment,vcContactName,vcContactPhone,vcContactEmail,vcCallRefNo,datFollowUp) values ("
                           + "{0},{1},{2},{3},{4},{5},{6},{7})",
                            txtNumInitialRow.Text.Equals("") ? "NULL" : txtNumInitialRow.Text,
                            txtVcFuCommentBy.Text.Equals("") ? "NULL" : "'" + txtVcFuCommentBy.Text + "'",
                            txtVcFuComment.Text.Equals("") ? "NULL" : "'" + txtVcFuComment.Text + "'",
                            txtVcContactName.Text.Equals("") ? "NULL" : "'" + txtVcContactName.Text + "'",
                            txtVcContactPhone.Text.Equals("") ? "NULL" : "'" + txtVcContactPhone.Text + "'",
                            txtVcContactEmail.Text.Equals("") ? "NULL" : "'" + txtVcContactEmail.Text + "'",
                            txtVcCallRefNo.Text.Equals("") ? "NULL" : "'" + txtVcCallRefNo.Text + "'",
                            txtDatFollowUp.Text.Equals("") ? "NULL" : "'" + dateValue.ToString().Split()[0] + "'"
                            );

                    DataSet ds = GridDataTable(InsertQuery);

                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();

                    DataList4.DataSource = ds.Tables[2];
                    DataList4.DataBind();

                    txtVcCallRefNo.Text = "";
                    txtVcContactName.Text = "";
                    txtVcContactPhone.Text = "";
                    txtVcContactEmail.Text = "";
                    txtVcFuCommentBy.Text = "";
                    txtDatFollowUp.Text = "";
                    txtVcFuComment.Text = "";
                }
            }
        }

        protected void InsertButton_Click(object sender, EventArgs e)
        {
            Control control = null;
            if (GridView1.FooterRow != null)
            {
                control = GridView1.FooterRow;
            }
            else
            {
                control = GridView1.Controls[0].Controls[0];
            }

            TextBox txtVcCommentBy = control.FindControl("footerVcCommentBy") as TextBox;
            TextBox txtVcComment = control.FindControl("footerVcComment") as TextBox;
            TextBox txtVcAcctNo = control.FindControl("footerVcAcctNo") as TextBox;
            TextBox txtVcClient = control.FindControl("footerVcClient") as TextBox;
            TextBox txtVcPatName = control.FindControl("footerVcPatName") as TextBox;
            TextBox txtVcPatSSN = control.FindControl("footerVcPatSSN") as TextBox;
            TextBox txtVcPatIns = control.FindControl("footerVcPatIns") as TextBox;
            TextBox txtVcPatInsIdNo = control.FindControl("footerVcPatInsIdNo") as TextBox;
            TextBox txtDecTotalChgs = control.FindControl("footerDecTotalChgs") as TextBox;
            TextBox txtDecExpected = control.FindControl("footerDecExpected") as TextBox;
            TextBox txtVcUpCategory = control.FindControl("footerVcUpCategory") as TextBox;

            String InsertQuery = string.Format(
               "Insert into Initial(vcCommentBy,vcComment,vcAcctNo,vcClient,vcPatName,vcPatSSN, vcPatIns,vcPatInsIdNo,"
                   + "decTotalChgs,decExpected,vcUpCategory) values ("
                   + "'{0}','{1}',{2},{3},{4},{5},{6},{7},{8},{9},{10}) ",
                    txtVcCommentBy.Text,
                    txtVcComment.Text,
                    txtVcAcctNo.Text.Equals("(Optional)") ? "NULL" : "'" + txtVcAcctNo.Text + "'",
                    txtVcClient.Text.Equals("(Optional)") ? "NULL" : "'" + txtVcClient.Text + "'",
                    txtVcPatName.Text.Equals("(Optional)") ? "NULL" : "'" + txtVcPatName.Text + "'",
                    txtVcPatSSN.Text.Equals("(Optional)") ? "NULL" : "'" + txtVcPatSSN.Text + "'",
                    txtVcPatIns.Text.Equals("(Optional)") ? "NULL" : "'" + txtVcPatIns.Text + "'",
                    txtVcPatInsIdNo.Text.Equals("(Optional)") ? "NULL" : "'" + txtVcPatInsIdNo.Text + "'",
                    txtDecTotalChgs.Text.Equals("(Optional)") ? "NULL" : "'" + txtDecTotalChgs.Text + "'",
                    txtDecExpected.Text.Equals("(Optional)") ? "NULL" : "'" + txtDecExpected.Text + "'",
                    txtVcUpCategory.Text.Equals("(Optional)") ? "NULL" : "'" + txtVcUpCategory.Text + "'"
               );
            string connectionstring = ConfigurationManager.ConnectionStrings["CommentsConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(InsertQuery, conn);
                comm.CommandType = CommandType.Text;
                comm.ExecuteNonQuery();
            }

            btnClear_Click(null, null);
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            txtCustomSQL.Text = "";

            Session["AccountsTable"] = GridDataTable().Tables[0];
            GridView1.DataSource = Session["AccountsTable"];
            GridView1.DataBind();

            GridView2.DataSource = null;
            GridView2.DataBind();

            DataList2.DataSource = null;
            DataList2.DataBind();

            DataList4.DataSource = null;
            DataList4.DataBind();
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            if (sender is LinkButton)
                txtSearch.Text = ((LinkButton)sender).Text;

            viewAcct();
        }

        protected void viewAcct()
        {
            DataSet ds = GridDataTable();

            // Persist the table in the Session object,
            // this is important for correct sorting
            Session["AccountsTable"] = ds.Tables[0];

            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();

            if (!txtSearch.Text.Equals(""))
            {

                GridView2.DataSource = ds.Tables[1];
                GridView2.DataBind();

                DataList2.DataSource = ds.Tables[1];
                DataList2.DataBind();

                DataList4.DataSource = ds.Tables[2];
                DataList4.DataBind();

                txtNumInitialRow.Text = ds.Tables[1].Rows[0]["numRow"].ToString();
            }
        }

        protected void btnCustomSQL_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";

            string sSQLTail = txtCustomSQL.Text;

            if (!sSQLTail.Equals(""))
            {
                if (sSQLTail.Length >= 6)
                {
                    if (sSQLTail.Substring(0, 6).ToUpper().Equals("DELETE"))
                    {
                        sSQLTail = sSQLTail.Replace("DELETE", "DELETE FROM Initial WHERE");

                        using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CommentsConnectionString"].ConnectionString))
                        {
                            using (var cmd = new SqlCommand(sSQLTail, conn))
                            {
                                cmd.CommandType = CommandType.Text;
                                conn.Open();
                                int rowsAffected = cmd.ExecuteNonQuery();
                                conn.Close();
                            }
                        }

                        txtCustomSQL.Text = "";
                    }
                }
            }

            DataSet ds = GridDataTable();

            // Persist the table in the Session object,
            // this is important for correct sorting
            Session["AccountsTable"] = ds.Tables[0];

            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
        }

        public String prefixOrSuffixIfDataExists(String sPrefix, String sData, String sSuffix)
        {
            String sRet = sData;

            if (!sData.Equals(""))
            {
                sRet = sPrefix + sRet + sSuffix;
            }

            return sRet;
        }

        protected void ExportToExcel_Click(object sender, EventArgs e)
        {
            var dtInitial = GetInitial();
            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Products");
            var totalCols = dtInitial.Columns.Count;
            var totalRows = dtInitial.Rows.Count;

            for (var col = 1; col <= totalCols; col++)
            {
                workSheet.Cells[1, col].Value = dtInitial.Columns[col - 1].ColumnName;
            }

            for (var row = 1; row <= totalRows; row++)
            {
                for (var col = 0; col < totalCols; col++)
                {
                    if (col == 0)
                    {
                        workSheet.Cells[row + 1, col + 1].Value = dtInitial.Rows[row - 1][col].ToString()
                            .Replace("||", Environment.NewLine + Environment.NewLine).Replace("|", Environment.NewLine + "   ");
                    }
                    else if (col == 7)
                    {
                        workSheet.Cells[row + 1, col + 1].Formula = "HYPERLINK(\""
                            + "http://192.168.161.126/Default.aspx?vcAcctNo=" + dtInitial.Rows[row - 1][col]
                            + "\",\""
                            + dtInitial.Rows[row - 1][col].ToString()
                            + "\")";
                    }
                    else
                    {
                        workSheet.Cells[row + 1, col + 1].Value = dtInitial.Rows[row - 1][col];
                    }
                }
            }

            workSheet.Column(2).Style.Numberformat.Format = "yyyy-mm-dd";
            workSheet.Column(4).Style.Numberformat.Format = "yyyy-mm-dd";
            workSheet.Column(8).Style.Font.UnderLine = true;
            workSheet.Column(8).Style.Font.Color.SetColor(Color.Blue);

            workSheet.Cells.AutoFitColumns();

            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=query.xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }

        public DataTable GetInitial()
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CommentsConnectionString"].ConnectionString))
            using (var cmd = new SqlCommand(sSQLSelectAllAccounts(), conn))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                var products = new DataTable();
                adapter.Fill(products);
                return products;
            }
        }
    }
}