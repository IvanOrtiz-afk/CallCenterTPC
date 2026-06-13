<%@ Page Title="Nuevo Tipo de Incidencia" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormTipoIncidencia.aspx.cs" Inherits="CallCenterTPC.FormTipoIncidencia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row justify-content-center mt-4">
        <div class="col-md-6"> <div class="card shadow-sm">
                <div class="card-header bg-success text-white">
                    <h4 class="mb-0">
                        <span class="material-symbols-outlined align-text-bottom me-2">add_circle</span>
                        Registrar Tipo de Incidencia
                    </h4>
                </div>
                <div class="card-body">

                    <asp:Panel ID="pnlMensaje" runat="server" Visible="false">
                        <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
                        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                    </asp:Panel>

                    <asp:Panel ID="pnlError" runat="server" Visible="false">
                        <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                     </asp:Panel>
                    
                    <div class="mb-4">
                        <label class="form-label">Nombre del Tipo de Incidencia</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ej: Falla Técnica, Consulta, Reclamo..."></asp:TextBox>
                    </div>

                    <div class="d-grid gap-2 d-md-flex justify-content-md-end">
                        <a href="TiposIncidencia.aspx" class="btn btn-outline-secondary">Cancelar</a>
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar Tipo" CssClass="btn btn-success" OnClick="btnGuardar_Click" />
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>