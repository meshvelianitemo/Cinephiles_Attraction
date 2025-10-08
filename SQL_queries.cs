using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
namespace WindowsFormsApp1
{
    internal class SQL_queries
    {
        static string query { get; set; }
        private string connection_string = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\source\repos\WindowsFormsApp1\WindowsFormsApp1\Database1.mdf;Integrated Security=True";


        public DataTable fetch_full_data()
        {
            query = "SELECT * FROM Films";
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connection_string))
            {
                // Open connection
                conn.Open();

                // Create command with your query and connection
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // SqlDataAdapter fills DataTable with query results
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;

        }

        public DataTable fetch_data_by_genre(string genre)
        {
            query = "SELECT * FROM Films WHERE Genre = @genre";
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connection_string))
            {
                // Open connection
                conn.Open();
                // Create command with your query and connection
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add parameter to avoid SQL injection
                    cmd.Parameters.AddWithValue("@genre", genre);
                    // SqlDataAdapter fills DataTable with query results
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable Check_authentification(string username, string password, Label message_label)
        {
            query = "SELECT * FROM Users WHERE Username = @username AND Password = @password";
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connection_string))
            {
                // Open connection
                conn.Open();
                // Create command with your query and connection
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add parameters to avoid SQL injection
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    // SqlDataAdapter fills DataTable with query results
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            /*if (dt.Rows.Count > 0)
            {
                message_label.Text = "Login successful!";
            }
            else
            {
                message_label.Text = "Invalid username or password.";
            }*/
            return dt;
        }

        public DataTable Insert_new_user(string username, string password)
        {
            query = "INSERT INTO Users (Username, Password) VALUES (@username, @password)";
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connection_string))
            {
                // Open connection
                conn.Open();
                // Create command with your query and connection
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add parameters to avoid SQL injection
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    // SqlDataAdapter fills DataTable with query results
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }
    }
}
