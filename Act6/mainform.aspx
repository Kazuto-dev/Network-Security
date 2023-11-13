<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mainform.aspx.cs" Inherits="Act6.mainform" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Security-Policy" content="@cspHeaderValue"/>
    <title>Logs</title>
</head>
<body>
    <h1>Hello World!!</h1>
    <form id="form1" runat="server">
        <div>
            <ul id="logList"></ul>

            <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
            <script src="https://cdnjs.cloudflare.com/ajax/libs/microsoft-signalr/3.1.11/signalr.min.js"></script>
            <script nonce="@nonce">
                $(document).ready(function () {
                    const connection = new signalR.HubConnectionBuilder()
                        .withUrl('/LogHub') // Specify the URL of your SignalR hub
                        .build();
                         
                    connection.on('logReceived', function (message) {
                        $('#logList').append('<li>' + message + '</li>');
                    });

                    connection.start().catch(function (err) {
                        return console.error(err.toString());
                    });
                });
            </script>
          </div>
    </form>
</body>
</html>