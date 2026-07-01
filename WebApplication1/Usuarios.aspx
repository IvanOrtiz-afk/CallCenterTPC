<%@ Page Title="Gestión de Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="CallCenterTPC.Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

       <asp:Panel ID="Panel1" runat="server" Visible="false">
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
        </asp:Panel>
    
    <div class="row align-items-center mt-4 mb-4">
        <div class="col-md-6">
            <h3><span class="material-symbols-outlined align-text-bottom me-2">manage_accounts</span>Usuarios</h3>
        </div>
        <div class="col-md-6 text-md-end">
            <a href="FormUsuarios.aspx" class="btn btn-dark shadow-sm">
                <span class="material-symbols-outlined align-text-bottom me-1">add_circle</span>
                Nuevo Usuario
            </a>
        </div>
    </div>

    <asp:Panel ID="pnlMensaje" runat="server" Visible="false">
        <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    </asp:Panel>

    <div class="card shadow-sm">
        <div class="card-body p-0">
            <asp:GridView ID="dgvUsuarios" runat="server" 
    AllowPaging="True" 
    PageSize="10" 
    OnPageIndexChanging="dgvUsuarios_PageIndexChanging"
    CssClass="table table-striped table-hover mb-0" 
    AutoGenerateColumns="False" 
    GridLines="None">
    
    <HeaderStyle CssClass="table-dark" />
    
    <%-- Estilo para el paginador --%>
    <PagerStyle HorizontalAlign="Center" CssClass="PaginadorBootstrap" />
    
    <Columns>
        <asp:BoundField DataField="id" HeaderText="ID" />
        <asp:BoundField DataField="nombre" HeaderText="Nombre" />
        <asp:BoundField DataField="apellido" HeaderText="Apellido" />
        <asp:BoundField DataField="email" HeaderText="Email" />
        <asp:BoundField DataField="rolId" HeaderText="Rol ID" />

        <asp:TemplateField HeaderText="Acciones">
            <ItemTemplate>
                <asp:HyperLink ID="lnkEditar" runat="server" 
                    Text="Editar" 
                    CssClass="btn btn-warning btn-sm me-2" 
                    NavigateUrl='<%# "FormUsuarios.aspx?id=" + Eval("id") %>'>
                </asp:HyperLink>

                <asp:LinkButton
                    ID="btnEliminar"
                    runat="server"
                    Text="Eliminar"
                    CssClass="btn btn-danger btn-sm"
                    CommandArgument='<%# Eval("id") %>'
                    OnClick="btnEliminar_Click"
                    OnClientClick="return confirm('¿Está seguro que desea dar de baja este usuario?');" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
        </div>
    </div>
</asp:Content>