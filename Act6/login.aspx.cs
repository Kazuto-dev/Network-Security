using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Web.Routing;
using System.Web.Security.AntiXss;
using System.IO;
using Serilog;
using System.Web.Security;
using System.Configuration;

namespace Act6
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login_Click(object sender, EventArgs e)
        {
            string username = AntiXssEncoder.HtmlEncode(usernames.Text, false);
            string password = AntiXssEncoder.HtmlEncode(passtext.Text, false);

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Jandell\source\repos\Act6\Act6\App_Data\Database2.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Use parameterized query to prevent SQL injection
                string query = "SELECT * FROM tbl WHERE username=@username AND password=@password";
                SqlCommand cmd = new SqlCommand(query, connection);

                // Add parameters to the query
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                SqlDataReader srd = cmd.ExecuteReader();

                if (srd.Read())
                {
                    // Log successful login
                    Log.Information("User {Username} logged in successfully", username);
                    // Log the user in

                    FormsAuthentication.RedirectFromLoginPage(username, false);

                    // Log the login activity
                    LogUserActivity("Login", username);
                    // Redirect to the encrypted URL
                    Response.Redirect("~/mainform.aspx");
                }
                else
                {
                    // Log failed login attempt
                    Log.Warning("Failed login attempt for user: {Username}", username);

                    lblMessage.Text = "Invalid username or password";
                    lblMessage.CssClass = "error-message";
                }

                connection.Close();
            }
        }

        private void LogUserActivity(string action, string username)
        {
            // Get the connection string from web.config
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Jandell\source\repos\Act6\Act6\App_Data\Database2.mdf;Integrated Security=True";

            // Insert the log into the UserLog table
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string insertQuery = "INSERT INTO userLog (UserId, Action, Timestamp) VALUES (@UserId, @Action, @Timestamp)";

                using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@UserId", username);
                    cmd.Parameters.AddWithValue("@Action", action);
                    cmd.Parameters.AddWithValue("@Timestamp", DateTime.Now);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

}