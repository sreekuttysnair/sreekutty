using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace Document_archieving_system
{
    public partial class questionupload : System.Web.UI.Page
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
            string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string contentType = FileUpload1.PostedFile.ContentType;
            FileUpload1.SaveAs(Server.MapPath("~/questions/") + filename);

            using (Stream fs = FileUpload1.PostedFile.InputStream)
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=dossier;User ID=sa;Password=admin123");
                    con.Open();
                    string sql = "insert into tbl_upload(dept_id,dept_name,cors_id,cors_name,sem_id,sub_id,sub_name,content_type,data,filename,batch_id,batch_name,qtype_id,date_time)values('" + ddl_dept.SelectedItem.Value + "','" + ddl_dept.SelectedItem.Value + "','" + ddl_cors.SelectedItem.Value + "','" + ddl_cors.SelectedItem.Text + "','" + ddl_sem.SelectedItem.Value + "','" + ddl_sub.SelectedItem.Value + "','" + ddl_sub.SelectedItem.Text + "','" + FileUpload1.FileContent + "','" + FileUpload1.FileBytes + "','" + FileUpload1.FileName + "','" + ddl_batch.SelectedItem.Value + "','" + ddl_batch.SelectedItem.Text + "','" + ddl_qtype.SelectedItem.Value + "',(GETDATE()))";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    ddl_cors.ClearSelection();
                    ddl_dept.ClearSelection();
                    ddl_batch.ClearSelection();
                    ddl_sem.ClearSelection();
                    ddl_sub.ClearSelection();
                    ddl_qtype.ClearSelection();
                    Response.Write("<script>alert('Successfully Inserted')</script>");
                    viewgrid();

                }
            }
        }

        protected void ddl_dept_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=dossier;User ID=sa;Password=admin123");

            con.Open();
            string com = "Select cors_id,cors_name from tbl_cors where dept_id='"+ddl_dept.SelectedItem.Value+"'";
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
            string com = "Select batch_id,batch_name from tbl_batch";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddl_batch.DataSource = dt;
            ddl_batch.DataTextField = "batch_name";
            ddl_batch.DataValueField = "batch_id";
            ddl_batch.DataBind();
            con.Close();
        }

        protected void ddl_batch_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void ddl_sem_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=dossier;User ID=sa;Password=admin123");

            con.Open();
            string com = "Select sub_id,sub_name from tbl_sub";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddl_sub.DataSource = dt;
            ddl_sub.DataTextField = "sub_name";
            ddl_sub.DataValueField = "sub_id";
            ddl_sub.DataBind();
            con.Close();
        }

        protected void ddl_sub_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=dossier;User ID=sa;Password=admin123");

            con.Open();
            string com = "Select qtype_id,qtype_name from tbl_qtype";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddl_qtype.DataSource = dt;
            ddl_qtype.DataTextField = "qtype_name";
            ddl_qtype.DataValueField = "qtype_id";
            ddl_qtype.DataBind();
            con.Close();
        }
        public void viewgrid() //course grid function
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=dossier;User ID=sa;Password=admin123");
            con.Open();
            SqlCommand cmd = new SqlCommand("select dept_name,cors_name,batch_name,sub_name,date_time,filename from tbl_upload", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmd.ExecuteNonQuery();
            GridView1.DataSource = dt;

            GridView1.DataBind();
        }

    }
}