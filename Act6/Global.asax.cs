using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using Microsoft.AspNet.SignalR;
using Serilog;
using Serilog.Events;






namespace Act6

{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            CreateRolesAndAssignUsers();

            // Initialize SignalR
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<LogHub>();

            // Configure Serilog with the custom SignalR sink
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.Sink(new SignalRSink(hubContext)) // Use the custom SignalR sink
                .CreateLogger();

            // Log the application start event
            Log.Information("Application started");

            // Specify the full path for the log file in the Downloads folder
            string logFilePath = Path.Combine(@"C:\Users\Jandell\Downloads", "log.txt");

            // Serilog configuration
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(logFilePath)
                .WriteTo.Sink(new SignalRSink(hubContext)) // Use the custom SignalR sink
                .CreateLogger();

            // Log the application start event
            Log.Information("Application started");
        }




        private void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            try
            {
                // Generate a random nonce value
                Random random = new Random();
                string nonce = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                
                // Get the Content-Security-Policy-Report-Only header value
                string cspHeaderValue = $"default-src 'self'; script-src 'self' 'nonce-{nonce}' 'https://cdnjs.cloudflare.com' 'https://code.jquery.com';";
                // Adjust this based on your actual CSP policy

                // Log the CSP header and its value
                Log.Information("Content-Security-Policy-Report-Only header: {CspHeaderValue}", cspHeaderValue);

                // Add Content-Security-Policy-Report-Only header
                HttpContext.Current.Response.Headers.Add("Content-Security-Policy-Report-Only", cspHeaderValue);
            }
            catch (Exception ex)
            {
                // Log any exceptions that occur during header processing
                Log.Error(ex, "Error in Application_PreSendRequestHeaders");
            }
        }


        private void CreateRolesAndAssignUsers()
        {
            // Create roles
            if (!Roles.RoleExists("Admin"))
            {
                Roles.CreateRole("Admin");
            }

            if (!Roles.RoleExists("User"))
            {
                Roles.CreateRole("User");
            }

            // Assign users to roles
            string adminUsername = "admin"; // Replace with your admin username
            if (!Roles.IsUserInRole(adminUsername, "Admin"))
            {
                Roles.AddUserToRole(adminUsername, "Admin");
            }

            // You can add more users and roles as needed
        }

        protected void Application_End(object sender, EventArgs e)
        {
            // Log the application shutdown event
            Log.Information("Application stopped");

            // Close and flush the log
            Log.CloseAndFlush();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            // Log the beginning of each HTTP request
            Log.Information("Request started: {Url}", Request.Url);
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            // Log the end of each HTTP request
            Log.Information("Request ended: {Url}", Request.Url);
        }



    }
}