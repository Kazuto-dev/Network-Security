<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="true" CodeBehind="UpdateUser.aspx.cs" Inherits="Act6.update" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Update User</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Update User</h1>
        
        <div>
            <label for="txtUsername">Username:</label>
            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        
        <div>
            <label for="txtPassword">Password:</label>
            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
        </div>
        
         <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>

        <div>
            <asp:Button runat="server" Text="Update" CssClass="btn btn-primary" OnClick="Update_Click" />
        </div>
    </div>
</form>

</body>
</html>
