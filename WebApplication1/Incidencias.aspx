<%@ Page Title="Gestión de Incidencias" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Incidencias.aspx.cs" Inherits="CallCenterTPC.Incidencias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="row align-items-center mt-4 mb-4">
        <div class="col-md-6">
            <h3><span class="material-symbols-outlined align-text-bottom me-2">support_agent</span>Incidencias</h3>
        </div>
        <div class="col-md-6 text-md-end">
            <a href="FormIncidencias.aspx" class="btn btn-danger shadow-sm">
                <span class="material-symbols-outlined align-text-bottom me-1">add_circle</span>
                Nueva Incidencia
            </a>
        </div>
    </div>
    <div class="row mb-3">
    <div class="col-md-6">
        <div class="input-group">
            <span class="input-group-text bg-white">
                <span class="material-symbols-outlined" style="font-size: 18px;">search</span>
            </span>
            <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar por N° de ticket o cliente..."></asp:TextBox>
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-outline-secondary" OnClick="btnBuscar_Click" />
            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-outline-secondary" OnClick="btnLimpiar_Click" />
        </div>
    </div>
</div>
      <asp:Panel ID="pnlMensaje" runat="server" Visible="false">
     <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
     <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
 </asp:Panel>

    <div class="card shadow-sm">
        <div class="card-body p-0">
          <asp:GridView ID="dgvIncidencias" runat="server" 
    AllowPaging="True" 
    PageSize="10" 
    OnPageIndexChanging="dgvIncidencias_PageIndexChanging"
    OnRowDataBound="dgvIncidencias_RowDataBound"
    AutoGenerateColumns="False"
    CssClass="table table-striped table-hover mb-0">
    
    <HeaderStyle CssClass="table-dark" />
    
    <PagerStyle HorizontalAlign="Center" CssClass="PaginadorBootstrap" />
    
    <Columns>
        <asp:BoundField DataField="id" HeaderText="Ticket" />
        <asp:BoundField DataField="asunto" HeaderText="Asunto" />
        <asp:BoundField DataField="cliente.nombre" HeaderText="Cliente" /> 
        <asp:BoundField DataField="tipoIncidencia.nombre" HeaderText="Tipo" />
        <asp:BoundField DataField="prioridad.nombre" HeaderText="Prioridad" />
        <asp:BoundField DataField="estado.nombre" HeaderText="Estado" />
        <asp:BoundField DataField="fechaAlta" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
        
        <asp:TemplateField HeaderText="Acciones">
            <ItemTemplate>
                <asp:HyperLink ID="lnkVer" runat="server" CssClass="btn btn-sm btn-info text-white"
                    NavigateUrl='<%# "DetalleIncidencia.aspx?id=" + Eval("id") %>' ToolTip="Ver Detalle">
                    <span class="material-symbols-outlined align-middle" style="font-size: 18px;">visibility</span>
                </asp:HyperLink>
      
                <asp:HyperLink ID="lnkEditar" runat="server" CssClass="btn btn-sm btn-warning"
                    NavigateUrl='<%# "FormIncidencias.aspx?id=" + Eval("id") %>' ToolTip="Editar">
                    <span class="material-symbols-outlined align-middle" style="font-size: 18px;">edit</span>
                </asp:HyperLink>

                <asp:HyperLink ID="lnkResolver" runat="server" CssClass="btn btn-sm btn-success"
                    NavigateUrl='<%# "ResolverIncidencia.aspx?id=" + Eval("id") %>' ToolTip="Resolver">
                    <span class="material-symbols-outlined align-middle" style="font-size: 18px;">task_alt</span>
                </asp:HyperLink>

                <asp:HyperLink ID="lnkCerrar" runat="server" CssClass="btn btn-sm btn-danger"
                    NavigateUrl='<%# "CerrarIncidencia.aspx?id=" + Eval("id") %>' ToolTip="Cerrar Incidencia">
                    <span class="material-symbols-outlined align-middle" style="font-size: 18px;">block</span>
                </asp:HyperLink>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
        </div>
    </div>
</asp:Content>
