<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CallCenterTPC.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

    <title>Login - Call Center TPC</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined" rel="stylesheet" />

</head>

<body class="bg-light">

    <form id="form1" runat="server">

        <div class="container">

            <div class="row justify-content-center vh-100 align-items-center">

                <div class="col-md-5">

                    <div class="card shadow">

                        <div class="card-header bg-dark text-white text-center">
                            <h3 class="mb-0">
                                <span class="material-symbols-outlined align-middle me-2">
                                    support_agent
                                </span>
                                Call Center TPC
                            </h3>
                        </div>

                        <div class="card-body">

                            <div class="mb-3">
                                <label class="form-label">Email</label>

                                <asp:TextBox
                                    ID="txtEmail"
                                    runat="server"
                                    CssClass="form-control"
                                    placeholder="Ingrese su email" />
                            </div>

                            <div class="mb-3">
                                <label class="form-label">Contraseña</label>

                                <asp:TextBox
                                    ID="txtPassword"
                                    runat="server"
                                    TextMode="Password"
                                    CssClass="form-control"
                                    placeholder="Ingrese su contraseña" />
                            </div>

                            <asp:Label
                                ID="lblError"
                                runat="server"
                                CssClass="text-danger d-block mb-3" />

                            <div class="d-grid">

                                <asp:Button
                                    ID="btnIngresar"
                                    runat="server"
                                    Text="Ingresar"
                                    CssClass="btn btn-dark"
                                    OnClick="btnIngresar_Click" />

                            </div>

                        </div>

                    </div>

                </div>

            </div>

        </div>

    </form>

</body>
</html>