<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userlogs.aspx.cs" Inherits="Act6.userlogs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Log Viewer</h1>
            
            <asp:GridView ID="logGridView" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="Timestamp" HeaderText="Time" />
                    <asp:BoundField DataField="UserId" HeaderText="Username" />
                    <asp:BoundField DataField="Action" HeaderText="Action" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
