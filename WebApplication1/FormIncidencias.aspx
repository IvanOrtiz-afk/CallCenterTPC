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

                    <link rel="stylesheet" href="//code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css">
                    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
                    <script src="https://code.jquery.com/ui/1.13.2/jquery-ui.min.js"></script>

                    <div class="row">
                        <div class="col-md-6 mb-3">
                            <label class="form-label">Cliente</label>
                            <asp:TextBox ID="txtBuscarCliente" runat="server" CssClass="form-control" Placeholder="Ingrese Nombre, Apellido o DNI"></asp:TextBox>
                            <asp:DropDownList ID="ddlClientes" runat="server" CssClass="form-select d-none"></asp:DropDownList>
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

    <script>
        $(function () {
            $("#<%=txtBuscarCliente.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        url: "FormIncidencias.aspx/BuscarClientesAutocomplete",
                        data: '{term: "' + request.term + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.nombre + " " + item.apellido + " (DNI: " + item.documento + ")",
                                    value: item.id
                                }
                            }));
                        }
                    });
                },
                select: function (event, ui) {
                    $("#<%=txtBuscarCliente.ClientID%>").val(ui.item.label);
                    var ddl = $("#<%=ddlClientes.ClientID%>");
                    ddl.empty();
                    ddl.append($("<option selected='selected'></option>").val(ui.item.value).text(ui.item.label));
                    return false;
                },
                minLength: 2
            });
        });
    </script>
</asp:Content>