using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Act6
{
    public class LogHub : Hub
    {
        public void SendLog(string message)
        {
            Clients.All.logReceived(message);
        }
    }
}