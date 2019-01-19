using StudentWebForms.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StudentWebForms
{
    public partial class StudentView : System.Web.UI.Page
    {
        private readonly string mvcDbConnectionString = @"Server=DESKTOP-52LH9TL\SQLEXPRESS;Database=StudentMvc;Trusted_Connection=True;";

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public List<Student> studentsGrid_GetData()
        {
            List<Student> itemsList = new List<Student>();
            using (SqlConnection conn = new SqlConnection(mvcDbConnectionString))
            {
                string sql = @"SELECT * FROM dbo.Student;";

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand(sql, conn);

                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    var item = new Student();
                    item.Id = int.Parse(row["Id"].ToString());
                    item.LastName = row["LastName"].ToString();
                    item.FirstName = row["FirstName"].ToString();
                    item.City = row["City"].ToString();
                    if(!String.IsNullOrEmpty(row["Age"].ToString()))
                    {
                        item.Age = int.Parse(row["Age"].ToString());
                    } else
                    {
                        item.Age = null;
                    }
                    itemsList.Add(item);
                }
            }
            return itemsList;
        }

        protected void studentsGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void lbDeleteStudent_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)(sender);
            int studentId = Convert.ToInt32(btn.CommandArgument);
            using (SqlConnection conn = new SqlConnection(mvcDbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "DELETE FROM dbo.Student WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", studentId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    Response.Redirect("StudentView.aspx");
                }
            }
        }

        protected void lbEditStudent_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)(sender);
            int studentId = Convert.ToInt32(btn.CommandArgument);
            Response.Redirect("StudentEdit.aspx?id=" + studentId);
        }
    }
}