using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace WindowsFormsApp1
{
    internal class SQL_queries
    {
        private string connection_string = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True";
        // Fetch all films
        public DataTable FetchFullData()
        {
            string query = "SELECT * FROM Films";
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connection_string))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }

            return dt;
        }

        // Fetch films by genre
        public DataTable FetchDataByGenre(string genre)
        {
            string query = "SELECT * FROM Films WHERE Genre = @genre";
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connection_string))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@genre", genre);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }

            return dt;
        }

        // Check login/authentication
        public bool CheckAuthentification(string username, string password, Label message_label)
        {
            string query = "SELECT COUNT(*) FROM Users WHERE Username = @username AND Password = @password";
            int count = 0;

            using (SqlConnection conn = new SqlConnection(connection_string))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    count = (int)cmd.ExecuteScalar();
                }
            }

            if (count > 0)
            {
                message_label.Text = "Login successful!";
                return true;
            }
            else
            {
                message_label.Text = "Invalid username or password.";
                return false;
            }
        }

        // Insert a new user
        public bool InsertNewUser(string username, string password, Label message_label)
        {
            string query = "INSERT INTO Users (Username, Password) VALUES (@username, @password)";
            int rowsAffected = 0;

            using (SqlConnection conn = new SqlConnection(connection_string))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    try
                    {
                        rowsAffected = cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        // For example, violation of UNIQUE constraint
                        message_label.Text = $"Error: {ex.Message}";
                        return false;
                    }
                }
            }

            if (rowsAffected > 0)
            {
                message_label.Text = "User registered successfully!";
                return true;
            }
            else
            {
                message_label.Text = "User registration failed!";
                return false;
            }
        }
    }
}
