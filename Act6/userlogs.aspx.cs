using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace Act6
{
    public partial class userlogs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                // Populate the GridView with log data
                List<LogEntry> logEntries = GetLogEntries();
                logGridView.DataSource = logEntries;
                logGridView.DataBind();
            }
        }

        private class LogEntry
        {
            public int LogId { get; set; }
            public DateTime Timestamp { get; set; }
            public string UserId { get; set; }
            public string Action { get; set; }
        }

        private List<LogEntry> GetLogEntries()
        {
            // Get the connection string from web.config
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Jandell\source\repos\Act6\Act6\App_Data\Database2.mdf;Integrated Security=True";

            // Retrieve logs from the database
            List<LogEntry> logEntries = new List<LogEntry>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT LogId, UserId, Action, Timestamp FROM userLog ORDER BY Timestamp DESC";

                using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LogEntry logEntry = new LogEntry
                            {
                                LogId = Convert.ToInt32(reader["LogId"]),
                                UserId = Convert.ToString(reader["UserId"]),
                                Timestamp = Convert.ToDateTime(reader["Timestamp"]),
                                Action = Convert.ToString(reader["Action"])                               
                            };

                            logEntries.Add(logEntry);
                        }
                    }
                }
            }

            return logEntries;
        }

    }
}
