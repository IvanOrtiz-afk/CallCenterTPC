<%@ Page Title="Nueva Incidencia" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormIncidencias.aspx.cs" Inherits="CallCenterTPC.FormIncidencias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row justify-content-center mt-4">
        <div class="col-md-8">
            <div class="card shadow-sm">
               <div class="card-header bg-dark text-white">
                    <h4 class="mb-0">
                        <asp:Label ID="lblTitulo" runat="server" Text="Registrar Nueva Incidencia"></asp:Label>
                    </h4>
                </div>
                <div class="card-body">

                    <asp:Panel ID="pnlMensaje" runat="server" Visible="false">
                        <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
                        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                    </asp:Panel>
                    
                    <div class="row">
                        <div class="col-md-6 mb-3">
                            <label class="form-label">Cliente</label>
                            <asp:DropDownList ID="ddlClientes" runat="server" CssClass="form-select"></asp:DropDownList>
                        </div>

                        <div class="col-md-6 mb-3">
                            <label class="form-label">Tipo de Incidencia</label>
                            <asp:DropDownList ID="ddlTipos" runat="server" CssClass="form-select"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6 mb-3">
                            <label class="form-label">Prioridad</label>
                            <asp:DropDownList ID="ddlPrioridades" runat="server" CssClass="form-select"></asp:DropDownList>
                        </div>

                    <div class="mb-3">
                    <label class="form-label">Asunto</label>

                    <asp:TextBox
                    ID="txtAsunto"
                    runat="server"
                    CssClass="form-control"
                    MaxLength="100"
                    placeholder="Ej.: No puede imprimir desde Windows 11">
                    </asp:TextBox>
                    </div>

                    <div class="mb-4">
                        <label class="form-label">Descripción de la Incidencia</label>
                        <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control" placeholder="Detalle el problema del cliente..."></asp:TextBox>
                    </div>

                    <div class="d-grid gap-2 d-md-flex justify-content-md-end">
                        <a href="Default.aspx" class="btn btn-outline-secondary">Cancelar</a>
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar Incidencia" CssClass="btn btn-danger" OnClick="btnGuardar_Click" />
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>