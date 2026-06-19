<%@ Page Title="Formulario de Cliente" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormClientes.aspx.cs" Inherits="CallCenterTPC.FormClientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row justify-content-center mt-4">
        <div class="col-md-8">
            <div class="card shadow-sm">
                
                <div class="card-header bg-primary text-white">
                    <h4 class="mb-0">
                        <span class="material-symbols-outlined align-text-bottom me-2">person_add</span>
                        <asp:Label ID="lblTitulo" runat="server" Text="Registrar Nuevo Cliente"></asp:Label>
                    </h4>
                </div>
                
                <div class="card-body">
                    
                    <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="alert alert-danger alert-dismissible fade show" role="alert">
                        <span class="material-symbols-outlined align-text-bottom me-2">error</span>
                        <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                    </asp:Panel>

                    <div class="row">
                        <div class="col-md-6 mb-3">
                            <label class="form-label">Nombre</label>
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ej: Juan"></asp:TextBox>
                        </div>

                        <div class="col-md-6 mb-3">
                            <label class="form-label">Apellido</label>
                            <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" placeholder="Ej: Pérez"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6 mb-3">
                            <label class="form-label">Documento (DNI)</label>
                            <asp:TextBox ID="txtDocumento" runat="server" TextMode="Number" CssClass="form-control" placeholder="Sin puntos ni espacios"></asp:TextBox>
                        </div>

                        <div class="col-md-6 mb-3">
                            <label class="form-label">Teléfono</label>
                            <asp:TextBox ID="txtTelefono" runat="server" TextMode="Number" CssClass="form-control" placeholder="Código de área + número"></asp:TextBox>
                        </div>
                    </div>

                    <div class="mb-4">
                        <label class="form-label">Correo Electrónico</label>
                        <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control" placeholder="ejemplo@correo.com"></asp:TextBox>
                    </div>

                    <div class="d-grid gap-2 d-md-flex justify-content-md-end mt-4">
                        <a href="Clientes.aspx" class="btn btn-outline-secondary">Cancelar</a>
                        
                        <asp:Button ID="btnEliminar" runat="server" Text="Dar de Baja" CssClass="btn btn-danger me-auto" Visible="false" OnClick="btnEliminar_Click" />
                        
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cliente" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>