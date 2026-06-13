<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CallCenterTPC._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

   <asp:Panel ID="pnlMensaje" runat="server" Visible="false">
        <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    </asp:Panel>

    <div class="row mt-4 mb-5">
        <div class="col-12 text-center">
            <h1 class="display-4">Bienvenido al Sistema</h1>
            <p class="lead text-muted">Seleccione una opción para comenzar a gestionar el Call Center.</p>
        </div>
    </div>

    <div class="row text-center justify-content-center">

        <div class="col-md-4 col-sm-6 mb-4">
            <a href="FormIncidencias.aspx" class="btn btn-outline-danger btn-lg w-100 py-4 shadow-sm">
                <span class="material-symbols-outlined fs-1 d-block mb-2">support_agent</span>
                Crear Incidencia
            </a>
        </div>
        
        <div class="col-md-4 col-sm-6 mb-4">
            <a href="FormClientes.aspx" class="btn btn-outline-primary btn-lg w-100 py-4 shadow-sm">
                <span class="material-symbols-outlined fs-1 d-block mb-2">group</span>
                Crear Cliente
            </a>
        </div>

       

        

        <!--<div class="col-md-4 col-sm-6 mb-4">
            <a href="Prioridades.aspx" class="btn btn-outline-warning btn-lg w-100 py-4 shadow-sm text-dark">
                <span class="material-symbols-outlined fs-1 d-block mb-2">warning</span>
                Prioridades
            </a>
        </div>

             <div class="col-md-4 col-sm-6 mb-4">
     <a href="Usuarios.aspx" class="btn btn-outline-success btn-lg w-100 py-4 shadow-sm">
         <span class="material-symbols-outlined fs-1 d-block mb-2">manage_accounts</span>
         Gestión de Usuarios
     </a>
 </div>

        <div class="col-md-4 col-sm-6 mb-4">
            <a href="TiposIncidencia.aspx" class="btn btn-outline-info btn-lg w-100 py-4 shadow-sm text-dark">
                <span class="material-symbols-outlined fs-1 d-block mb-2">category</span>
                Tipos de Incidencia
            </a>
        </div> -->

    </div>

</asp:Content>