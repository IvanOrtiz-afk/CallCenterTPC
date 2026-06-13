<%@ Page Title="Nueva Prioridad" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormPrioridad.aspx.cs" Inherits="CallCenterTPC.FormPrioridad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row justify-content-center mt-4">
        <div class="col-md-6">
            <div class="card shadow-sm">
                <div class="card-header bg-warning text-dark"><h4>Registrar Prioridad</h4></div>
                <div class="card-body">
                    <asp:Panel ID="pnlMensaje" runat="server" Visible="false">
                        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    </asp:Panel>
                    
                    <div class="mb-3">
                        <label class="form-label">Nombre de la Prioridad</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    
                    <div class="d-grid d-md-flex justify-content-md-end">
                        <a href="Prioridades.aspx" class="btn btn-outline-secondary me-2">Cancelar</a>
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-warning" OnClick="btnGuardar_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
