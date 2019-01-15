using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using ToDoMvc.Models;

namespace ToDoMvc.Controllers
{
    public class ToDoController : Controller
    {
        private readonly string mvcDbConnectionString = @"Server=DESKTOP-52LH9TL\SQLEXPRESS;Database=MvcVjezba;Trusted_Connection=True;";
        
        public ActionResult Index()
        {
            List<ToDoItem> itemsList = new List<ToDoItem>();
            using (SqlConnection conn = new SqlConnection(mvcDbConnectionString))
            {
                string sql = @"SELECT * FROM dbo.ToDoItem;";

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand(sql, conn);

                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    var item = new ToDoItem();
                    item.Id = Int32.Parse(row["Id"].ToString());
                    item.CreatedDate = Convert.ToDateTime(row["CreatedDate"]);
                    item.Description = row["Description"].ToString();
                    item.Done = Convert.ToBoolean(row["Done"]);
                    itemsList.Add(item);
                }
            }
            return View(itemsList);
        }
        
        public ActionResult AddItem(string itemDescription)
        {
            using (SqlConnection conn = new SqlConnection(mvcDbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO dbo.TodoItem(Description, CreatedDate, Done) VALUES(@description, @createdDate, @done)";

                    cmd.Parameters.AddWithValue("@description", itemDescription);
                    cmd.Parameters.AddWithValue("@createdDate", DateTime.UtcNow);
                    cmd.Parameters.AddWithValue("@done", false);

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

        public ActionResult DeleteItem(int itemId)
        {
            using (SqlConnection conn = new SqlConnection(mvcDbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "DELETE FROM dbo.TodoItem WHERE Id = @itemId";

                    cmd.Parameters.AddWithValue("@itemId", itemId);

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

        public ActionResult SetAsDone(int itemId, bool isDone)
        {
            using (SqlConnection conn = new SqlConnection(mvcDbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE dbo.TodoItem SET Done = @isDone WHERE Id = @itemId";

                    cmd.Parameters.AddWithValue("@isDone", !isDone);
                    cmd.Parameters.AddWithValue("@itemId", itemId);

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