using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Act6
{
    public partial class mainform : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Bind the repeater with data
                UserRepeater.DataSource = YourDataFetchingMethod();
                UserRepeater.DataBind();
            }
        }


        // Example method to fetch data from the database
        public List<User> YourDataFetchingMethod()
        {
            List<User> userList = new List<User>();

            // Replace the connection string with your actual database connection string
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Jandell\\source\\repos\\Act6\\Act6\\App_Data\\Database2.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Replace "YourTableName" with the actual name of your table
                string query = "SELECT Id, Username, Password FROM tbl2";
                SqlCommand command = new SqlCommand(query, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Assuming you have a User class
                        User user = new User
                        {
                            Id = reader.GetInt32(0),
                            Username = reader.GetString(1),
                            Password = reader.GetString(2)
                        };

                        userList.Add(user);
                    }
                }
            }

            return userList;
        }

        // Example User class
        public class User
        {
            public int Id { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
        }

        protected void UserRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int userId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Update")
            {
                // Find the controls within the repeater item for the edited values
                TextBox txtUsername = (TextBox)e.Item.FindControl("txtUsername"); // Replace "txtUsername" with the ID of your username textbox
                TextBox txtPassword = (TextBox)e.Item.FindControl("txtPassword"); // Replace "txtPassword" with the ID of your password textbox

                // Get the updated values
                string updatedUsername = txtUsername.Text;
                string updatedPassword = txtPassword.Text;

                // Call a method to update the user with the specified ID
                UpdateUser(userId, updatedUsername, updatedPassword);

                // Rebind the Repeater after updating
                BindUserRepeater();
            }
            else if (e.CommandName == "Delete")
            {
                // Call a method to delete the user with the specified ID
                DeleteUser(userId);

                // Rebind the Repeater after deletion
                BindUserRepeater();
            }
        }



        public void UpdateUser(int userId, string updatedUsername, string updatedPassword)
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Jandell\\source\\repos\\Act6\\Act6\\App_Data\\Database2.mdf;Integrated Security=True"; // Replace with your actual connection string
            string updateQuery = "UPDATE tbl2 SET Username = @Username, Password = @Password WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(updateQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", userId);
                    cmd.Parameters.AddWithValue("@Username", updatedUsername);
                    cmd.Parameters.AddWithValue("@Password", updatedPassword);

                    // Execute the UPDATE query
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Log success or perform any other actions
                        LogUserActivity("User with ID {Username} updated successfully.", userId.ToString());
                    }
                    else
                    {
                        LogUserActivity("Update user failed.", userId.ToString());
                    }
                }
            }
        }


        protected void UpdateUser(object sender, EventArgs e)
        {
            // Get the user ID from the command argument
            int userId = Convert.ToInt32((sender as Button).CommandArgument);

            // Redirect to the update page or show a modal for update
            Response.Redirect($"UpdateUser.aspx?UserId={userId}");
        }


        public void DeleteUser(int userId)
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Jandell\\source\\repos\\Act6\\Act6\\App_Data\\Database2.mdf;Integrated Security=True"; // Replace with your actual connection string
            string deleteQuery = "DELETE FROM tbl2 WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(deleteQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", userId);

                    // Execute the DELETE query
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Log success or perform any other actions
                     
                        LogUserActivity("User with ID {UserId} deleted successfully.", userId.ToString());
                    }
                    else
                    {
                        // Log failure or perform any other actions
                        
                    }
                }
            }
        }

        private void BindUserRepeater()
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Jandell\\source\\repos\\Act6\\Act6\\App_Data\\Database2.mdf;Integrated Security=True"; // Replace with your actual connection string
            string selectQuery = "SELECT Id, username, password FROM tbl2"; // Replace with your actual table name

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Bind the result set to the repeater control
                        UserRepeater.DataSource = reader;
                        UserRepeater.DataBind();
                    }
                }
            }
        }
        public void LogUserActivity(string action, string username)
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

        protected void Update_Click(object sender, EventArgs e)
        {
            Response.Redirect("userlogs.aspx");
        }
    }
}
