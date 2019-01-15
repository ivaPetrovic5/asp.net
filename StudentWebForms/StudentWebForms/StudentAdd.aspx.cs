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
    public partial class StudentAdd : System.Web.UI.Page
    {
        private readonly string mvcDbConnectionString = @"Server=DESKTOP-52LH9TL\SQLEXPRESS;Database=StudentMvc;Trusted_Connection=True;";

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void createStudent_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(mvcDbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO dbo.Student(FirstName, LastName, City, Age) VALUES(@firstName, @lastName, @city, @age)";

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