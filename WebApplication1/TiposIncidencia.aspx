<%@ Page Title="Tipos de Incidencia" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TiposIncidencia.aspx.cs" Inherits="CallCenterTPC.TiposIncidencia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <asp:Panel ID="pnlMensaje" runat="server" Visible="false">
      <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
      <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
  </asp:Panel>
    
    <div class="row align-items-center mt-4 mb-4">
        <div class="col-md-6">
            <h3 class="mb-0">
                <span class="material-symbols-outlined align-text-bottom me-2">category</span>
                Tipos de Incidencia
            </h3>
        </div>
        <div class="col-md-6 text-md-end mt-3 mt-md-0">
            <a href="FormTipoIncidencia.aspx" class="btn btn-success shadow-sm">
                <span class="material-symbols-outlined align-text-bottom me-1">add_circle</span>
                Crear Nuevo Tipo
            </a>
        </div>
    </div>

    <div class="row">
        <div class="col-12">
            <div class="card shadow-sm">
                <div class="card-body p-0"> <asp:GridView ID="dgvTiposIncidencia" runat="server" 
    AllowPaging="True" 
    PageSize="10" 
    OnPageIndexChanging="dgvTiposIncidencia_PageIndexChanging"
    CssClass="table table-striped table-hover mb-0" 
    AutoGenerateColumns="False" 
    GridLines="None" 
    EmptyDataText="No hay tipos de incidencia registrados.">
    
    <HeaderStyle CssClass="table-dark" />
    
    <PagerStyle HorizontalAlign="Center" CssClass="PaginadorBootstrap" />
    
    <Columns>
        <asp:BoundField DataField="id" HeaderText="ID" ItemStyle-Width="10%" />
        <asp:BoundField DataField="nombre" HeaderText="Nombre del Tipo de Incidencia" />
    </Columns>

</asp:GridView>

                </div>
            </div>
        </div>
    </div>

</asp:Content>
