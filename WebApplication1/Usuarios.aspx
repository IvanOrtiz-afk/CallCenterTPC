<%@ Page Title="Gestión de Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="CallCenterTPC.Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

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
            <div class="p-3 border-bottom">
                <div class="row align-items-center">
                    <div class="col-md-6 mb-2 mb-md-0">
                        <div class="input-group">
                            <span class="input-group-text bg-white">
                                <span class="material-symbols-outlined" style="font-size: 18px;">search</span>
                            </span>
                            <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar por ID, nombre o email..."></asp:TextBox>
                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-outline-secondary" OnClick="btnBuscar_Click" />
                            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-outline-secondary" OnClick="btnLimpiar_Click" />
                        </div>
                    </div>
                    <div class="col-md-6 text-md-end">
                        <asp:CheckBox ID="chkVerInactivos" runat="server" Text=" Mostrar usuarios inactivos" AutoPostBack="true" OnCheckedChanged="chkVerInactivos_CheckedChanged" CssClass="form-check-input me-2" />
                    </div>
                </div>
            </div>

            <asp:GridView ID="dgvUsuarios" runat="server" CssClass="table table-striped table-hover mb-0"
                AutoGenerateColumns="False" GridLines="None">
                <HeaderStyle CssClass="table-dark" />
                <Columns>

                    <asp:BoundField DataField="id" HeaderText="ID" />
                    <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="apellido" HeaderText="Apellido" />
                    <asp:BoundField DataField="email" HeaderText="Email" />
                    <asp:BoundField DataField="rolId" HeaderText="Rol ID" />

                    <asp:TemplateField HeaderText="Estado">
                        <ItemTemplate>
                            <span class='<%# Convert.ToBoolean(Eval("activo")) ? "badge bg-success" : "badge bg-secondary" %>'>
                                <%# Convert.ToBoolean(Eval("activo")) ? "Activo" : "Inactivo" %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:HyperLink ID="lnkEditar" runat="server"
                                Text="Editar"
                                CssClass="btn btn-warning btn-sm me-1"
                                Visible='<%# Convert.ToBoolean(Eval("activo")) %>'
                                NavigateUrl='<%# "FormUsuarios.aspx?id=" + Eval("id") %>'>
                            </asp:HyperLink>

                            <button type="button"
                                class='<%# Convert.ToBoolean(Eval("activo")) ? "btn btn-danger btn-sm btn-cambiar-estado" : "btn btn-success btn-sm btn-cambiar-estado" %>'
                                data-id='<%# Eval("id") %>'
                                data-mensaje='<%# Convert.ToBoolean(Eval("activo")) ? "¿Confirmás que querés dar de baja a este usuario?" : "¿Confirmás que querés reactivar a este usuario?" %>'>
                                <%# Convert.ToBoolean(Eval("activo")) ? "Dar de baja" : "Reactivar" %>
                            </button>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>
    </div>

    <div class="modal fade" id="modalConfirmacion" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Confirmar acción</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <p id="lblModalTexto">¿Confirmás que querés cambiar el estado de este usuario?</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-outline-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <button type="button" id="btnConfirmarModal" class="btn btn-danger">Confirmar</button>
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hfIdSeleccionado" runat="server" />
    <asp:LinkButton ID="lnkConfirmarCambioEstado" runat="server" OnClick="lnkConfirmarCambioEstado_Click" style="display:none;"></asp:LinkButton>

    <script>
        document.addEventListener('click', function (e) {
            if (e.target.classList.contains('btn-cambiar-estado')) {
                var id = e.target.getAttribute('data-id');
                var mensaje = e.target.getAttribute('data-mensaje');

                document.getElementById('lblModalTexto').innerText = mensaje;
                document.getElementById('<%=hfIdSeleccionado.ClientID%>').value = id;

                var modal = new bootstrap.Modal(document.getElementById('modalConfirmacion'));
                modal.show();
            }
        });

        document.getElementById('btnConfirmarModal').addEventListener('click', function () {
            var modalEl = document.getElementById('modalConfirmacion');
            bootstrap.Modal.getInstance(modalEl).hide();
            document.getElementById('<%=lnkConfirmarCambioEstado.ClientID%>').click();
        });
    </script>

</asp:Content>