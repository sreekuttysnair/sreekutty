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
    public partial class course1 : System.Web.UI.Page
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
            string sql = "insert into tbl_cors(cors_name,dept_id,no_sem)values('" + txt_cors.Text + "','" + ddl_dept.SelectedItem.Value + "','" + txt_sem.Text + "')";
            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.ExecuteNonQuery();
            txt_cors.Text = "";
            txt_sem.Text = "";
            ddl_dept.ClearSelection();
            Response.Write("<script>alert('Successfully Inserted')</script>"); 
        }
    }
}