using StudentMvc.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace StudentMvc.Controllers
{
    public class StudentController : Controller
    {
        private readonly string mvcDbConnectionString = @"Server=DESKTOP-52LH9TL\SQLEXPRESS;Database=StudentMvc;Trusted_Connection=True;";

        public ActionResult Index()
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
                    item.Age = int.Parse(row["Age"].ToString());
                    itemsList.Add(item);
                }
            }
            return View(itemsList);
        }

        public ActionResult Create(Student model)
        {
            using (SqlConnection conn = new SqlConnection(mvcDbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO dbo.Student(FirstName, LastName, City, Age) VALUES(@firstName, @lastName, @city, @age)";

                    cmd.Parameters.AddWithValue("@firstName", model.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", model.LastName);
                    cmd.Parameters.AddWithValue("@city", model.City);
                    cmd.Parameters.AddWithValue("@age", model.Age);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return RedirectToAction("Index");
                    }
                    catch (SqlException e)
                    {
                        return View();
                    }
                }
            }
        }

        public ActionResult Delete(int studentId)
        {
            using (SqlConnection conn = new SqlConnection(mvcDbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "DELETE FROM dbo.Student WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", studentId);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return RedirectToAction("Index");
                    }
                    catch (SqlException e)
                    {
                        return View();
                    }
                }
            }
        }
    }
}