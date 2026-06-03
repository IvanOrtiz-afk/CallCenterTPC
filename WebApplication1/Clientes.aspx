<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="CallCenterTPC.Clientes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">

        <h2>Listado de Clientes</h2>

        <asp:GridView ID="dgvClientes"
            runat="server"
            AutoGenerateColumns="true">
        </asp:GridView>

    </div>
</form>
</body>
</html>
