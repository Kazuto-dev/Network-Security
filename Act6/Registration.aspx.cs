using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security.AntiXss;

namespace Act6
{
    [Authorize(Roles = "Admin")]
    public partial class Registration : System.Web.UI.Page
    {
        protected void Registration_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Jandell\source\repos\Act6\Act6\App_Data\Database2.mdf;Integrated Security=True";
            RegistrationManager registrationManager = new RegistrationManager(connectionString);

            string username = AntiXssEncoder.HtmlEncode(usernames.Text, false);
            string password = AntiXssEncoder.HtmlEncode(passtext.Text, false);

            bool registrationSuccess = registrationManager.RegisterUser(username, password);

            if (registrationSuccess)
            {
                // Registration successful
                // Log the login activity
                LogUserActivity("Registered", username);
                lblMessage.Text = "User registration successful. You can now log in.";
                lblMessage.CssClass = "success-message";
            }
            else
            {
                // Registration failed
                LogUserActivity("Registration failed", username);
                lblMessage.Text = "Registration failed. Please try again.";
                lblMessage.CssClass = "error-message";
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