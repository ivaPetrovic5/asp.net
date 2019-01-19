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
                    if(!String.IsNullOrEmpty(row["Age"].ToString()))
                    {
                        item.Age = int.Parse(row["Age"].ToString());
                    }
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

        public ActionResult Edit(int studentId)
        {
            Student student = new Student();
            using (SqlConnection conn = new SqlConnection(mvcDbConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM dbo.Student WHERE Id = @id;", conn);
                command.Parameters.AddWithValue("@id", studentId);
                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        student.Id = int.Parse(reader["Id"].ToString());
                        student.FirstName = reader["FirstName"].ToString();
                        student.LastName = reader["LastName"].ToString();
                        student.City = reader["City"].ToString();
                        if (!String.IsNullOrEmpty(reader["Age"].ToString()))
                        {
                            student.Age = int.Parse(reader["Age"].ToString());
                        }
                        return View(student);
                    }
                }
                return View();
            }
        }

        [HttpPost]
        public ActionResult Edit(Student student)
        {
            using (SqlConnection conn = new SqlConnection(mvcDbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE dbo.Student SET FirstName = @firstName, LastName = @lastName, Age = @age, City = @city WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", student.Id);
                    cmd.Parameters.AddWithValue("@firstName", student.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", student.LastName);
                    cmd.Parameters.AddWithValue("@age", student.Age);
                    cmd.Parameters.AddWithValue("@city", student.City);

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