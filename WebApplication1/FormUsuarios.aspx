<%@ Page Title="Nuevo Usuario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormUsuarios.aspx.cs" Inherits="CallCenterTPC.FormUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row justify-content-center mt-4">
        <div class="col-md-6">
            <div class="card shadow-sm">
               <div class="card-header bg-dark text-white">
                    <h4>
                        <span class="material-symbols-outlined align-text-bottom me-2">manage_accounts</span>
                        <asp:Label ID="lblTitulo" runat="server" Text="Registrar Usuario"></asp:Label>
                    </h4>
                </div>
                <div class="card-body">
                    
                    <asp:Panel ID="pnlMensaje" runat="server" Visible="false">
                        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    </asp:Panel>

                    <div class="row">
                        <div class="col-md-6 mb-3">
                            <label class="form-label">Nombre</label>
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-6 mb-3">
                            <label class="form-label">Apellido</label>
                            <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Email (Usuario)</label>
                        <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Contraseña</label>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="mb-4">
                        <label class="form-label">Rol</label>
                        <asp:DropDownList ID="ddlRoles" runat="server" CssClass="form-select"></asp:DropDownList>
                    </div>

                    <div class="d-grid d-md-flex justify-content-md-end">
                      <a href="Usuarios.aspx" class="btn btn-outline-secondary me-2">Cancelar</a>
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar Usuario" CssClass="btn btn-dark" OnClick="btnGuardar_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
