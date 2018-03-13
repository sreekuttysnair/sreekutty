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
    public partial class subject1 : System.Web.UI.Page
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
            string sql = "insert into tbl_sub(sub_code ,sub_name,sem_name,dept_id ,cors_id,cors_name)values('" + txt_subcod.Text + "','" + txt_sub.Text + "','" + ddl_sem.SelectedItem.Text + "','" + ddl_dept.SelectedItem.Value + "','" + ddl_cors.SelectedItem.Value + "','" + ddl_cors.SelectedItem.Text + "')";
            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.ExecuteNonQuery();
            txt_sub.Text = "";
            txt_subcod.Text = "";
            ddl_cors.ClearSelection();
            ddl_dept.ClearSelection();
            ddl_schm.ClearSelection();
            ddl_sem.ClearSelection();
            Response.Write("<script>alert('Successfully Inserted')</script>");
        }

        protected void ddl_dept_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=dossier;User ID=sa;Password=admin123");

            con.Open();
            string com = "Select cors_id,cors_name from tbl_cors where dept_id='"+ddl_dept.SelectedItem.Value+"' ";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddl_cors.DataSource = dt;
            ddl_cors.DataTextField = "cors_name";
            ddl_cors.DataValueField = "cors_id";
            ddl_cors.DataBind();
            con.Close();
        }

        protected void ddl_cors_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=dossier;User ID=sa;Password=admin123");

            con.Open();
            string com = "Select schm_id,schm_name from tbl_scheme";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddl_schm.DataSource = dt;
            ddl_schm.DataTextField = "schm_name";
            ddl_schm.DataValueField = "schm_id";
            ddl_schm.DataBind();
            con.Close();
        }

        protected void ddl_schm_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=dossier;User ID=sa;Password=admin123");

            con.Open();
            string com = "Select sem_id,sem_name from tbl_sem";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddl_sem.DataSource = dt;
            ddl_sem.DataTextField = "sem_name";
            ddl_sem.DataValueField = "sem_id";
            ddl_sem.DataBind();
            con.Close();

        }
    }
}