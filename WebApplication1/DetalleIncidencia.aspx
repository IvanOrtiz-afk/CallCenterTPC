<%@ Page Title="Detalle de Incidencia"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="DetalleIncidencia.aspx.cs"
    Inherits="CallCenterTPC.DetalleIncidencia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row justify-content-center mt-4">
        <div class="col-lg-10">

            <div class="card shadow-sm">

                <div class="card-header bg-dark text-white">
                    <h4 class="mb-0">
                        <span class="material-symbols-outlined align-text-bottom me-2">
                            support_agent
                        </span>
                        Detalle de Incidencia
                    </h4>
                </div>

                <div class="card-body">

                    <asp:Panel ID="pnlMensaje" runat="server" Visible="false">
                        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    </asp:Panel>

                    <div class="row">

                        <div class="col-md-6 mb-3">
                            <label class="form-label fw-bold">ID</label>
                            <asp:Label ID="lblId" runat="server" CssClass="form-control"></asp:Label>
                        </div>

                        <div class="col-md-6 mb-3">
                            <label class="form-label fw-bold">Estado</label>
                            <asp:Label ID="lblEstado" runat="server" CssClass="form-control"></asp:Label>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-md-6 mb-3">
                            <label class="form-label fw-bold">Cliente</label>
                            <asp:Label ID="lblCliente" runat="server" CssClass="form-control"></asp:Label>
                        </div>

                        <div class="col-md-6 mb-3">
                            <label class="form-label fw-bold">Tipo de Incidencia</label>
                            <asp:Label ID="lblTipo" runat="server" CssClass="form-control"></asp:Label>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-md-6 mb-3">
                            <label class="form-label fw-bold">Prioridad</label>
                            <asp:Label ID="lblPrioridad" runat="server" CssClass="form-control"></asp:Label>
                        </div>

                        <div class="col-md-6 mb-3">
                            <label class="form-label fw-bold">Fecha de Alta</label>
                            <asp:Label ID="lblFechaAlta" runat="server" CssClass="form-control"></asp:Label>
                        </div>

                    </div>

                    <div class="row">

                    <div class="col-md-6 mb-3">
                    <label class="form-label fw-bold">Creada por</label>
                    <asp:Label ID="lblUsuarioCreador"
                    runat="server"
                    CssClass="form-control" />
                    </div>

                    <div class="col-md-6 mb-3">
                    <label class="form-label fw-bold">Asignada a</label>
                    <asp:Label ID="lblUsuarioAsignado"
                    runat="server"
                    CssClass="form-control" />
                    </div>

                    <asp:Panel ID="pnlAsignar" runat="server" Visible="false">

    <hr />

    <div class="row">

        <div class="col-md-8">

            <label class="form-label fw-bold">
                Asignar incidencia a
            </label>

            <asp:DropDownList
                ID="ddlAgentes"
                runat="server"
                CssClass="form-select">
            </asp:DropDownList>

        </div>

        <div class="col-md-4 d-flex align-items-end">

            <asp:Button
                ID="btnAsignar"
                runat="server"
                Text="Asignar"
                CssClass="btn btn-primary w-100"
                OnClick="btnAsignar_Click" />

        </div>

    </div>

</asp:Panel>

                    </div>

                    <div class="mb-3">
                        <label class="form-label fw-bold">Descripción</label>
                        <asp:TextBox
                            ID="txtDescripcion"
                            runat="server"
                            CssClass="form-control"
                            Rows="5"
                            TextMode="MultiLine"
                            ReadOnly="true">
                        </asp:TextBox>
                    </div>

                    <div class="d-flex justify-content-end">

                        <a href="Incidencias.aspx" class="btn btn-secondary">
                            Volver
                        </a>
                        <asp:Button 
                            ID="btnReabrir" 
                            runat="server" 
                            Text="Reabrir Incidencia" 
                            CssClass="btn btn-warning shadow-sm" 
                            Visible="false" 
                            OnClick="btnReabrir_Click" />

                    </div>

                </div>

            </div>

        </div>
    </div>

</asp:Content>