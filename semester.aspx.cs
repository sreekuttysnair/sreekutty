using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


namespace Document_archieving_system
{
    public partial class semester : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=dossier;User ID=sa;Password=admin123");

                con.Open();
                string com = "Select dept_id,dept_name from tbl_dept";
                SqlDataAdapter adpt = new SqlDataAdapter(com, con);
                DataTable dt = new DataTable();
                adpt.Fill(dt);
                ddl_dept.DataSource = dt;
                ddl_dept.DataTextField = "dept_name";
                ddl_dept.DataValueField = "dept_id";
                ddl_dept.DataBind();

                con.Close();
            }
        }

        protected void btn_sav_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=dossier;User ID=sa;Password=admin123");
            con.Open();
            string sql = "insert into tbl_sem(sem_name,dept_id,cors_id)values('"+txt_sem.Text+"','"+ddl_dept.SelectedItem.Value+"','"+ddl_cors.SelectedItem.Value+"')";
            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.ExecuteNonQuery();
            txt_sem.Text = "";
           
            ddl_cors.ClearSelection();
            ddl_dept.ClearSelection();
            Response.Write("<script>alert('Successfully Inserted')</script>");
        }

        protected void ddl_dept_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=dossier;User ID=sa;Password=admin123");

            con.Open();
            string com = "Select cors_id,cors_name from tbl_cors";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddl_cors.DataSource = dt;
            ddl_cors.DataTextField = "cors_name";
            ddl_cors.DataValueField = "cors_id";
            ddl_cors.DataBind();
            con.Close();
        }
    }
}