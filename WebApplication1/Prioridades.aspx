<%@ Page Title="Gestión de Prioridades" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Prioridades.aspx.cs" Inherits="CallCenterTPC.Prioridades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="row align-items-center mt-4 mb-4">
        <div class="col-md-6">
            <h3><span class="material-symbols-outlined align-text-bottom me-2">warning</span>Prioridades</h3>
        </div>
        <div class="col-md-6 text-md-end">
            <a href="FormPrioridad.aspx" class="btn btn-warning shadow-sm"><span class="material-symbols-outlined align-text-bottom me-1">add_circle</span>Crear Nueva Prioridad</a>
        </div>
    </div>

    <asp:Panel ID="pnlMensaje" runat="server" Visible="false">
        <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    </asp:Panel>

    <div class="card shadow-sm">
        <div class="card-body p-0">
           <asp:GridView ID="dgvPrioridades" runat="server" 
    AllowPaging="True" 
    PageSize="10" 
    OnPageIndexChanging="dgvPrioridades_PageIndexChanging"
    CssClass="table table-striped table-hover mb-0" 
    AutoGenerateColumns="False">
    
    <HeaderStyle CssClass="table-dark" />
    
    <PagerStyle HorizontalAlign="Center" CssClass="PaginadorBootstrap" />
    
    <Columns>
        <asp:BoundField DataField="id" HeaderText="ID" />
        <asp:BoundField DataField="nombre" HeaderText="Nombre de Prioridad" />
    </Columns>
</asp:GridView>
        </div>
    </div>
</asp:Content>