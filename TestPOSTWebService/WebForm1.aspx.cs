using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace TestPOSTWebService
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
                BindGridData();
        }

        protected void GridView1_RowEditing(Object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            BindGridData();
        }

        protected void GridView1_RowCancelling(Object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            BindGridData();
        }

        protected void GridView1_RowDeleting(Object sender, GridViewDeleteEventArgs e)
        {
            string numRow = GridView1.DataKeys[e.RowIndex].Value.ToString();
            string Query = "Delete Simple WHERE Simple.numRow=" + numRow;
            BindGridData(Query);
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string numRow = GridView1.DataKeys[e.RowIndex].Value.ToString();
            TextBox txtVcCommentBy = GridView1.Rows[e.RowIndex].FindControl("txtVcCommentBy") as TextBox;
            TextBox txtVcComment = GridView1.Rows[e.RowIndex].FindControl("txtVcComment") as TextBox;
            TextBox txtVcRefNo = GridView1.Rows[e.RowIndex].FindControl("txtVcRefNo") as TextBox;
            TextBox txtDatFollowUp = GridView1.Rows[e.RowIndex].FindControl("txtDatFollowUp") as TextBox;
            TextBox txtVcNotes = GridView1.Rows[e.RowIndex].FindControl("txtVcNotes") as TextBox;

            String UpdateQuery = string.Format(
                "UPDATE Simple SET "
                    + "vcCommentBy='{0}',"
                    + "vcComment='{1}',"
                    + "vcRefNo={2},"
                    + "datFollowUp={3},"
                    + "vcNotes={4} "
                + "WHERE numRow={5}",
                    txtVcCommentBy.Text,
                    txtVcComment.Text,
                    txtVcRefNo.Text.Equals("") ? "NULL" : "'" + txtVcRefNo.Text + "'",
                    txtDatFollowUp.Text.Equals("") ? "NULL" : "'" + txtDatFollowUp.Text + "'",
                    txtVcNotes.Text.Equals("") ? "NULL" : "'" + txtVcNotes.Text + "'",
                    Convert.ToInt32(numRow)
                );

            GridView1.EditIndex = -1;
            BindGridData(UpdateQuery);
        }

        private void BindGridData()
        {
            BindGridData("", "");
        }

        private void BindGridData(string Query)
        {
            BindGridData(Query, "");
        }

        private void BindGridData(string Query, string Source)
        {
            string sSource = "Select * From Simple";

            if(IsPostBack)
            {
                string sSearch = Request.Form[txtSearch.UniqueID];
                if(sSearch.Length > 0)
                {
                    sSource += " WHERE idClaim='" + sSearch + "'";
                }
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
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                }

            }
        }

        protected void InsertButton_Click(object sender, EventArgs e)
        {
            TextBox txtVcCommentBy = GridView1.FooterRow.FindControl("footerVcCommentBy") as TextBox;
            TextBox txtVcComment = GridView1.FooterRow.FindControl("footerVcComment") as TextBox;
            TextBox txtIdClaim = GridView1.FooterRow.FindControl("footerIdClaim") as TextBox;
            TextBox txtIdClient = GridView1.FooterRow.FindControl("footerVcClient") as TextBox;

            TextBox txtVcRefNo = GridView1.FooterRow.FindControl("footerVcRefNo") as TextBox;
            TextBox txtDatFollowUp = GridView1.FooterRow.FindControl("footerDatFollowUp") as TextBox;
            TextBox txtVcNotes = GridView1.FooterRow.FindControl("footerVcNotes") as TextBox;

            String InsertQuery = string.Format(
               "Insert into Simple(vcCommentBy,vcComment,vcRefNo,datFollowUp,vcNotes,idClaim,vcClient) values ("
                   + "'{0}',"
                   + "'{1}',"
                   + "{2},"
                   + "{3},"
                   + "{4},'{5}','{6}') ",
                   txtVcCommentBy.Text,
                   txtVcComment.Text,
                   txtVcRefNo.Text.Equals("(Optional)") ? "NULL" : "'" + txtVcRefNo.Text + "'",
                   txtDatFollowUp.Text.Equals("(Optional)") ? "NULL" : "'" + txtDatFollowUp.Text + "'",
                   txtVcNotes.Text.Equals("(Optional)") ? "NULL" : "'" + txtVcNotes.Text + "'",
                   txtIdClaim.Text, txtIdClient.Text
               );
            string connectionstring = ConfigurationManager.ConnectionStrings["CommentsConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(InsertQuery, conn);
                comm.CommandType = CommandType.Text;
                comm.ExecuteNonQuery();
            }
            BindGridData();
        }
    }


}