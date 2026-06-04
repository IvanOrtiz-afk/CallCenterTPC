<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CallCenterTPC._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4">

        <h1>CallCenterTPC</h1>

        <p>
            Sistema de gestión de incidencias para Call Center.
        </p>

        <hr />

        <h3>Módulos disponibles</h3>

        <ul>
            <li><a href="Clientes.aspx">Gestión de Clientes</a></li>
            <li><a href="Usuarios.aspx">Gestión de Usuarios</a></li>
            <li><a href="Incidencias.aspx">Gestión de Incidencias</a></li>
            <li><a href="Prioridades.aspx">Gestión de Prioridades</a></li>
            <li><a href="TiposIncidencia.aspx">Gestión de Tipos de Incidencia</a></li>
        </ul>

    </div>

</asp:Content>