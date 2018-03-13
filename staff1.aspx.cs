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
    public partial class staff1 : System.Web.UI.Page
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

        protected void ddl_dept_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btn_sav_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=dossier;User ID=sa;Password=admin123");
            con.Open();
            string sql = "insert into tbl_staff(staff_name,desig,status,dept_id,username,password,user_typ_name,dept_name)values('" + txt_staffnam.Text + "','" + ddl_desig.SelectedItem.Text + "','active','" + ddl_dept.SelectedItem.Value + "','" + txt_user.Text + "','" + txt_pass.Text + "','STAFF','" + ddl_dept.SelectedItem.Text + "')";
            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.ExecuteNonQuery();
            txt_pass.Text = "";
            txt_staffnam.Text = "";
            txt_user.Text = "";

            ddl_desig.ClearSelection();
            ddl_dept.ClearSelection();

            Response.Write("<script>alert('Successfully Inserted')</script>");
            viewgrid();
        }
        public void viewgrid() //course grid function
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=dossier;User ID=sa;Password=admin123");
            con.Open();
            SqlCommand cmd = new SqlCommand("select staff_id,staff_name,desig,dept_name from tbl_staff where status='active'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmd.ExecuteNonQuery();
            GridView1.DataSource = dt;

            GridView1.DataBind();
        }
    }
}