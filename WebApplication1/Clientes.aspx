<%@ Page Title="Gestión de Clientes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="CallCenterTPC.Clientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="row align-items-center mt-4 mb-4">
        <div class="col-md-6">
            <h3><span class="material-symbols-outlined align-text-bottom me-2">group</span>Clientes</h3>
        </div>
        <div class="col-md-6 text-md-end">
            <a href="FormClientes.aspx" class="btn btn-primary shadow-sm">
                <span class="material-symbols-outlined align-text-bottom me-1">add_circle</span>
                Nuevo Cliente
            </a>
        </div>
    </div>

    <asp:Panel ID="pnlMensaje" runat="server" Visible="false">
     <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
     <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    </asp:Panel>

    <div class="card shadow-sm">
        <div class="card-body p-0">
            <asp:CheckBox ID="chkVerInactivos" runat="server" Text=" Mostrar clientes inactivos" AutoPostBack="true" OnCheckedChanged="chkVerInactivos_CheckedChanged" CssClass="mb-3" />
            <asp:GridView ID="dgvClientes" runat="server" CssClass="table table-striped table-hover mb-0" AutoGenerateColumns="False">
                <HeaderStyle CssClass="table-dark" />
                <Columns>
                    <asp:BoundField DataField="id" HeaderText="ID" />
                    <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="apellido" HeaderText="Apellido" />
                    <asp:BoundField DataField="documento" HeaderText="Documento" />
                    <asp:BoundField DataField="email" HeaderText="Email" />
                    <asp:BoundField DataField="estadoTexto" HeaderText="Estado" />
                    
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <a href="FormClientes.aspx?id=<%# Eval("id") %>" class="btn btn-warning btn-sm">Modificar</a>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>