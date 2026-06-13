<%@ Page Title="Gestión de Incidencias" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Incidencias.aspx.cs" Inherits="CallCenterTPC.Incidencias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="row align-items-center mt-4 mb-4">
        <div class="col-md-6">
            <h3><span class="material-symbols-outlined align-text-bottom me-2">support_agent</span>Incidencias</h3>
        </div>
        <div class="col-md-6 text-md-end">
            <a href="FormIncidencias.aspx" class="btn btn-danger shadow-sm">
                <span class="material-symbols-outlined align-text-bottom me-1">add_circle</span>
                Nueva Incidencia
            </a>
        </div>
    </div>

      <asp:Panel ID="pnlMensaje" runat="server" Visible="false">
     <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
     <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
 </asp:Panel>

    <div class="card shadow-sm">
        <div class="card-body p-0">
            <asp:GridView ID="dgvIncidencias" runat="server" CssClass="table table-striped table-hover mb-0" 
                AutoGenerateColumns="False" OnRowDataBound="dgvIncidencias_RowDataBound">
                <HeaderStyle CssClass="table-dark" />
                <Columns>
                    <asp:BoundField DataField="id" HeaderText="ID" />
                    <asp:BoundField DataField="cliente.nombre" HeaderText="Cliente" /> <asp:BoundField DataField="tipoIncidencia.nombre" HeaderText="Tipo" />
                    <asp:BoundField DataField="prioridad.nombre" HeaderText="Prioridad" />
                    <asp:BoundField DataField="estado.nombre" HeaderText="Estado" />
                    <asp:BoundField DataField="fechaAlta" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
