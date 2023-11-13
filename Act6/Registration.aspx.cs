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
using Serilog;

namespace Act6
{
    [Authorize(Roles = "Admin")]
    public partial class Registration : System.Web.UI.Page
    {
        protected void Registration_Click(object sender, EventArgs e)
        {
            string username = AntiXssEncoder.HtmlEncode(usernames.Text, false);
            string password = AntiXssEncoder.HtmlEncode(passtext.Text, false);

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Jandell\source\repos\Act6\Act6\App_Data\Database2.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Begin the transaction
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Insert into tbl2 (Registration)
                    string insertQuery2 = "INSERT INTO tbl2 (username, password) VALUES (@username, @password); SELECT SCOPE_IDENTITY();";
                    SqlCommand insertCmd2 = new SqlCommand(insertQuery2, connection, transaction);

                    insertCmd2.Parameters.AddWithValue("@username", username);
                    insertCmd2.Parameters.AddWithValue("@password", password);

                    // Insert into tbl1 (Login)
                    string insertQuery1 = "INSERT INTO tbl (username, password) VALUES (@username, @password)";
                    SqlCommand insertCmd1 = new SqlCommand(insertQuery1, connection, transaction);

                    insertCmd1.Parameters.AddWithValue("@username", username);
                    insertCmd1.Parameters.AddWithValue("@password", password);

                    // Assign the "User" role to the registered user
                    if (Roles.RoleExists("User"))
                    {
                        Roles.AddUserToRole(username, "User");
                    }

                    // Execute the insert into tbl1
                    int rowsAffected1 = insertCmd1.ExecuteNonQuery();

                    // Commit the transaction
                    transaction.Commit();

                    if (rowsAffected1 > 0)
                    {
                        // Log successful registration
                        Log.Information("User {Username} registered successfully", username);

                        lblMessage.Text = "User registration successful. You can now log in.";
                        lblMessage.CssClass = "success-message";
                    }
                    else
                    {
                        // Log failed registration
                        Log.Warning("Failed to insert user {Username} into tbl1", username);

                        lblMessage.Text = "Registration failed. Please try again.";
                        lblMessage.CssClass = "error-message";
                    }

                }
                catch (Exception ex)
                {
                    // An error occurred, rollback the transaction
                    transaction.Rollback();

                    // Log the error
                    Log.Error(ex, "Error during registration for user: {Username}", username);

                    lblMessage.Text = "Registration failed. Please try again. Error: " + AntiXssEncoder.HtmlEncode(ex.Message, false);
                    lblMessage.CssClass = "error-message";
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}