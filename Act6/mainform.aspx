<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="true" CodeBehind="mainform.aspx.cs" Inherits="Act6.mainform" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Security-Policy" content="@cspHeaderValue"/>
      <link rel="preconnect" href="https://fonts.googleapis.com"/>
  <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin />
  <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@200&display=swap" rel="stylesheet"/>
  <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous"/>
    <title>Logs</title>
</head>
<body>
    <h1>Table Beyatch</h1>
    
    <form id="form1" runat="server">
         <asp:Button runat="server" Text="Logs" CssClass="btn btn-primary" OnClick="Update_Click" />
<asp:Repeater ID="UserRepeater" runat="server" OnItemCommand="UserRepeater_ItemCommand">
    <HeaderTemplate>
        <table class="table">
            <thead>
                <tr>
                    <th scope="col">#</th>
                    <th scope="col">Username</th>
                    <th scope="col">Password</th>
                    <th scope="col">Actions</th>
                </tr>
            </thead>
            <tbody>
    </HeaderTemplate>
<ItemTemplate>
    <tr>
        <td><%# Eval("Id") %></td>
        <td>
            <asp:TextBox ID="txtUsername" runat="server" Text='<%# Eval("Username") %>'></asp:TextBox>
        </td>
        <td>
            <asp:TextBox ID="txtPassword" runat="server" Text='<%# Eval("Password") %>'></asp:TextBox>
        </td>
        <td>
            <asp:Button runat="server" Text="Update" CssClass="btn btn-outline-dark btn-lg px-5" CommandName="Update" CommandArgument='<%# Eval("Id") %>' />
            <asp:Button runat="server" Text="Delete" CssClass="btn btn-outline-dark btn-lg px-5" CommandName="Delete" CommandArgument='<%# Eval("Id") %>' />
        </td>
    </tr>
</ItemTemplate>

    <FooterTemplate>
            </tbody>
        </table>
    </FooterTemplate>
</asp:Repeater>
        </form>
</body>
</html>