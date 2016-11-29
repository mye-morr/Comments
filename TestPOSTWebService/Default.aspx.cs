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

namespace TestPOSTWebService
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = GridDataTable();

                //Persist the table in the Session object.
                Session["CommentsTable"] = dt;

                //Bind the GridView control to the data source.
                GridView1.DataSource = Session["CommentsTable"];
                GridView1.DataBind();
            }
        }

        protected void Refresh_Sample_Dialog(DataTable dt)
        {
            TableHeaderRow thRow = new TableHeaderRow();
            TableHeaderCell thCell = new TableHeaderCell();
            thCell.Text = "Sample Comments Dialog";
            thRow.Cells.Add(thCell);
            Table1.Rows.Add(thRow);

            List<Comment> listComments = dt.DataTableToList<Comment>();
            foreach (Comment comment in listComments)
            {
                TableRow row = new TableRow();
                TableCell cell = new TableCell();

                StringBuilder sb = new StringBuilder("");
                sb.AppendFormat("{0}, {1}", comment.datComment, comment.vcCommentBy);

                sb.AppendLine("<br/><br/>");
                sb.AppendLine(comment.vcComment);
                cell.Text = sb.ToString();
                row.Cells.Add(cell);
                Table1.Rows.Add(row);
            }
        }

        protected void GridView1_RowEditing(Object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            DataTable dt = GridDataTable();
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void GridView1_RowCancelling(Object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            DataTable dt = GridDataTable();
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void GridView1_RowDeleting(Object sender, GridViewDeleteEventArgs e)
        {
            string numRow = GridView1.DataKeys[e.RowIndex].Value.ToString();
            string Query = "Delete Simple WHERE Simple.numRow=" + numRow;
            DataTable dt = GridDataTable(Query);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            string numRow = GridView1.DataKeys[e.RowIndex].Value.ToString();
            TextBox txtVcCommentBy = GridView1.Rows[e.RowIndex].FindControl("txtVcCommentBy") as TextBox;
            TextBox txtVcComment = GridView1.Rows[e.RowIndex].FindControl("txtVcComment") as TextBox;
            TextBox txtVcCategory = GridView1.Rows[e.RowIndex].FindControl("txtVcCategory") as TextBox;
            TextBox txtDecExpected = GridView1.Rows[e.RowIndex].FindControl("txtDecExpected") as TextBox;
            TextBox txtDecVariance = GridView1.Rows[e.RowIndex].FindControl("txtDecVariance") as TextBox;
            TextBox txtVcInsurance = GridView1.Rows[e.RowIndex].FindControl("txtVcInsurance") as TextBox;
            TextBox txtVcContactName = GridView1.Rows[e.RowIndex].FindControl("txtVcContactName") as TextBox;
            TextBox txtVcContactPhone = GridView1.Rows[e.RowIndex].FindControl("txtVcContactPhone") as TextBox;
            TextBox txtVcContactEmail = GridView1.Rows[e.RowIndex].FindControl("txtVcContactEmail") as TextBox;
            TextBox txtVcCallRefNo = GridView1.Rows[e.RowIndex].FindControl("txtVcCallRefNo") as TextBox;
            TextBox txtDatFollowUp = GridView1.Rows[e.RowIndex].FindControl("txtDatFollowUp") as TextBox;
            TextBox txtVcNotes = GridView1.Rows[e.RowIndex].FindControl("txtVcNotes") as TextBox;

            String UpdateQuery = string.Format(
                "UPDATE Simple SET "
                    + "vcCommentBy='{0}',"
                    + "vcComment='{1}',"
                    + "vcCategory={2},"
                    + "decExpected={3},"
                    + "decVariance={4},"
                    + "vcInsurance={5},"
                    + "vcContactName={6},"
                    + "vcContactPhone={7},"
                    + "vcContactEmail={8},"
                    + "vcCallRefNo={9},"
                    + "datFollowUp={10},"
                    + "vcNotes={11} "
                + "WHERE numRow={12}",
                    txtVcCommentBy.Text,
                    txtVcComment.Text,
                    txtVcCategory.Text.Equals("") ? "NULL" : "'" + txtVcCategory.Text + "'",
                    txtDecExpected.Text.Equals("") ? "NULL" : "'" + txtDecExpected.Text + "'",
                    txtDecVariance.Text.Equals("") ? "NULL" : "'" + txtDecVariance.Text + "'",
                    txtVcInsurance.Text.Equals("") ? "NULL" : "'" + txtVcInsurance.Text + "'",
                    txtVcContactName.Text.Equals("") ? "NULL" : "'" + txtVcContactName.Text + "'",
                    txtVcContactPhone.Text.Equals("") ? "NULL" : "'" + txtVcContactPhone.Text + "'",
                    txtVcContactEmail.Text.Equals("") ? "NULL" : "'" + txtVcContactEmail.Text + "'",
                    txtVcCallRefNo.Text.Equals("") ? "NULL" : "'" + txtVcCallRefNo.Text + "'",
                    txtDatFollowUp.Text.Equals("") ? "NULL" : "'" + txtDatFollowUp.Text + "'",
                    txtVcNotes.Text.Equals("") ? "NULL" : "'" + txtVcNotes.Text + "'",
                    Convert.ToInt32(numRow)
                );

            GridView1.EditIndex = -1;

            DataTable dt = GridDataTable(UpdateQuery);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }



        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {

            //Retrieve the table from the session object
            DataTable dt = Session["CommentsTable"] as DataTable;

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

        private DataTable GridDataTable()
        {
            return GridDataTable("");
        }

        private DataTable GridDataTable(string Query)
        {
            string sSource = "Select * From Simple";

            if (!txtSearch.Text.Equals(""))
            {
                sSource += " WHERE idClaim='" + txtSearch.Text + "'";
            }
                
            string connectionstring = ConfigurationManager.ConnectionStrings["CommentsConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                using (SqlCommand comm = new SqlCommand(Query + ";" + sSource, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(comm);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    return ds.Tables[0];
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
            TextBox txtIdClaim = control.FindControl("footerIdClaim") as TextBox;
            TextBox txtVcCategory = control.FindControl("footerVcCategory") as TextBox;
            TextBox txtDecExpected = control.FindControl("footerDecExpected") as TextBox;
            TextBox txtDecVariance = control.FindControl("footerDecVariance") as TextBox;
            TextBox txtVcClient = control.FindControl("footerVcClient") as TextBox;
            TextBox txtVcInsurance = control.FindControl("footerVcInsurance") as TextBox;
            TextBox txtVcContactName = control.FindControl("footerVcContactName") as TextBox;
            TextBox txtVcContactPhone = control.FindControl("footerVcContactPhone") as TextBox;
            TextBox txtVcContactEmail = control.FindControl("footerVcContactEmail") as TextBox;
            TextBox txtVcCallRefNo = control.FindControl("footerVcCallRefNo") as TextBox;
            TextBox txtDatFollowUp = control.FindControl("footerDatFollowUp") as TextBox;
            TextBox txtVcNotes = control.FindControl("footerVcNotes") as TextBox;

            String InsertQuery = string.Format(
               "Insert into Simple(vcCommentBy,vcComment,idClaim,vcCategory,decExpected, decVariance,vcClient,"
                   + "vcInsurance,vcContactName,vcContactPhone,vcContactEmail,vcCallRefNo,datFollowUp,vcNotes) values ("
                   + "'{0}','{1}',{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}) ",
                    txtVcCommentBy.Text,
                    txtVcComment.Text,
                    txtIdClaim.Text.Equals("(Optional)") ? "NULL" : "'" + txtIdClaim.Text + "'",
                    txtVcCategory.Text.Equals("(Optional)") ? "NULL" : "'" + txtVcCategory.Text + "'",
                    txtDecExpected.Text.Equals("(Optional)") ? "NULL" : "'" + txtVcCategory.Text + "'",
                    txtDecVariance.Text.Equals("(Optional)") ? "NULL" : "'" + txtVcCategory.Text + "'",
                    txtVcClient.Text.Equals("(Optional)") ? "NULL" : "'" + txtVcClient.Text + "'",
                    txtVcInsurance.Text.Equals("(Optional)") ? "NULL" : "'" + txtVcInsurance.Text + "'",
                    txtVcContactName.Text.Equals("(Optional)") ? "NULL" : "'" + txtVcContactName.Text + "'",
                    txtVcContactPhone.Text.Equals("(Optional)") ? "NULL" : "'" + txtVcContactPhone.Text + "'",
                    txtVcContactEmail.Text.Equals("(Optional)") ? "NULL" : "'" + txtVcContactEmail.Text + "'",
                    txtVcCallRefNo.Text.Equals("(Optional)") ? "NULL" : "'" + txtVcCallRefNo.Text + "'",
                    txtDatFollowUp.Text.Equals("(Optional)") ? "NULL" : "'" + txtDatFollowUp.Text + "'",
                    txtVcNotes.Text.Equals("(Optional)") ? "NULL" : "'" + txtVcNotes.Text + "'"
               );
            string connectionstring = ConfigurationManager.ConnectionStrings["CommentsConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(InsertQuery, conn);
                comm.CommandType = CommandType.Text;
                comm.ExecuteNonQuery();
            }

            DataTable dt = GridDataTable();
            Session["CommentsTable"] = dt;
            GridView1.DataSource = Session["CommentsTable"];
            GridView1.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = GridDataTable();
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            Label1.Visible = true;
            DataTable dt = GridDataTable();
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            DataTable dt = GridDataTable();
            GridView1.DataSource = dt;
            GridView1.DataBind();

            Label1.Visible = false;
            Refresh_Sample_Dialog(dt);
        }
    }
}