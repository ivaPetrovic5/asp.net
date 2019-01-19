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
    public partial class StudentEdit : System.Web.UI.Page
    {
        private readonly string mvcDbConnectionString = @"Server=DESKTOP-52LH9TL\SQLEXPRESS;Database=StudentMvc;Trusted_Connection=True;";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                var studentId = Request.QueryString["id"];
                hfId.Value = Request.QueryString["id"];
                using (SqlConnection conn = new SqlConnection(mvcDbConnectionString))
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM dbo.Student WHERE Id = @id;", conn);
                    command.Parameters.AddWithValue("@id", studentId);
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            tbFirstName.Text = reader["FirstName"].ToString();
                            tbLastName.Text = reader["LastName"].ToString();
                            tbCity.Text = reader["City"].ToString();
                            if (!String.IsNullOrEmpty(reader["Age"].ToString()))
                            {
                                tbAge.Text = reader["Age"].ToString();
                            }
                        }
                    }
                }
            }
        }

        protected void editStudent_Click(object sender, EventArgs e)
        {
            if(Page.IsPostBack)
            {
                using (SqlConnection conn = new SqlConnection(mvcDbConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "UPDATE dbo.Student SET FirstName = @firstName, LastName = @lastName, City = @city, Age = @age WHERE Id = @id";

                        cmd.Parameters.AddWithValue("@id", hfId.Value);
                        cmd.Parameters.AddWithValue("@firstName", tbFirstName.Text);
                        cmd.Parameters.AddWithValue("@lastName", tbLastName.Text);
                        cmd.Parameters.AddWithValue("@city", tbCity.Text);
                        cmd.Parameters.AddWithValue("@age", tbAge.Text);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        Response.Redirect("StudentView.aspx");
                    }
                }
            }
            
        }
    }
}