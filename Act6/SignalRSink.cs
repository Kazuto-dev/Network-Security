using System;
using Microsoft.AspNet.SignalR;
using Serilog;
using Serilog.Core;
using Serilog.Events;

public class SignalRSink : ILogEventSink
{
    private readonly IHubContext _hubContext;

    public SignalRSink(IHubContext hubContext)
    {
        _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
    }

    public void Emit(LogEvent logEvent)
    {
        // Convert the log event to a string
        string logMessage = logEvent.RenderMessage();

        // Send the log message to connected SignalR clients
        _hubContext.Clients.All.logReceived(logMessage);
    }
}
