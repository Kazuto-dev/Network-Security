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
using Serilog.Events;






namespace Act6

{
    public class Global : HttpApplication
    {
       public void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            CreateRolesAndAssignUsers();
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

                // Add Content-Security-Policy-Report-Only header
                HttpContext.Current.Response.Headers.Add("Content-Security-Policy-Report-Only", cspHeaderValue);
            }
            catch (Exception ex)
            {

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
    }
}