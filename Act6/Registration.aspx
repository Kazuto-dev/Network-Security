<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="Act6.Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <meta charset="UTF-8"/>
        <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
        <link rel="stylesheet" type="text/css" href="style.css"/>
        <link rel="preconnect" href="https://fonts.googleapis.com"/>
        <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin />
        <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@200&display=swap" rel="stylesheet"/>
        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous"/>
    <title>Registration Form</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <section class="vh-100 gradient-custom">
                  <div class="container py-5 h-100">
                    <div class="row d-flex justify-content-center align-items-center h-100">
                      <div class="col-12 col-md-8 col-lg-6 col-xl-5">
                        <div class="card bg-dark text-white" style="border-radius: 1rem;">
                          <div class="card-body p-5 text-center">

                            <div class="mb-md-5 mt-md-4 pb-5">

                              <h2 class="fw-bold mb-2 text-uppercase">Registration</h2>
                              <p class="text-white-50 mb-5">Please register your username and password!</p>

                              <div class="form-outline form-white mb-4">
                                <asp:TextBox runat="server" ID="usernames" CssClass="form-control form-control-lg"></asp:TextBox>
                                <asp:Label runat="server"  CssClass="form-label">Username</asp:Label>
                              </div>

                              <div class="form-outline form-white mb-4">
                                <asp:TextBox runat="server" TextMode="Password" ID="passtext" CssClass="form-control form-control-lg"></asp:TextBox>
                                <asp:Label runat="server" CssClass="form-label">Password</asp:Label>
                              </div>
                                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>


                              <p class="small mb-5 pb-lg-2"><a class="text-white-50" href="#!">Forgot password?</a></p>

                              <asp:Button runat="server" Text="Register" CssClass="btn btn-outline-light btn-lg px-5" OnClick="Registration_Click" />

                              <div class="d-flex justify-content-center text-center mt-4 pt-1">
                                <a href="#!" class="text-white"><i class="fab fa-facebook-f fa-lg"></i></a>
                                <a href="#!" class="text-white"><i class="fab fa-twitter fa-lg mx-4 px-2"></i></a>
                                <a href="#!" class="text-white"><i class="fab fa-google fa-lg"></i></a>
                              </div>
                            </div>
                        <div>
                          <p class="mb-0">Have an account? <a href="login.aspx" class="text-white-50 fw-bold">Log In </a></p>
                        </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </section>

        </div>
    </form>
</body>
</html>