using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace Document_archieving_system
{
    public partial class department : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_sav_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=dossier;User ID=sa;Password=admin123");
            con.Open();
            string sql = "insert into tbl_dept(dept_name)values('" + txt_dept.Text + "')";
            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.ExecuteNonQuery();
            txt_dept.Text = "";
            Response.Write("<script>alert('Successfully Inserted')</script>");

        }
    }
}