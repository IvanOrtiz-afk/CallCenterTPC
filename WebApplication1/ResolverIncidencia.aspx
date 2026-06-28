<%@ Page Title="Resolver Incidencia" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResolverIncidencia.aspx.cs" Inherits="CallCenterTPC.ResolverIncidencia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="row align-items-center mt-4 mb-4">
        <div class="col-12">
            <h3><span class="material-symbols-outlined align-text-bottom me-2">task_alt</span>Resolver Incidencia</h3>
            <p class="text-muted">Por favor, ingrese la solución o respuesta satisfactoria que se le dio al cliente.</p>
        </div>
    </div>

    <asp:Panel ID="pnlMensaje" runat="server" Visible="false">
        <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    </asp:Panel>

    <div class="card shadow-sm col-md-8 offset-md-2">
        <div class="card-body">
            
            <div class="mb-3">
                <label for="txtComentario" class="form-label fw-bold">Comentario de Resolución (Obligatorio)</label>
                <asp:TextBox ID="txtComentario" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" placeholder="Detalle la solución alcanzada..."></asp:TextBox>
            </div>

            <div class="d-flex justify-content-end gap-2 mt-4">
                <a href="Incidencias.aspx" class="btn btn-secondary shadow-sm">Cancelar</a>
                
                <asp:Button ID="btnGuardar" runat="server" Text="Confirmar Resolución" CssClass="btn btn-success shadow-sm" OnClick="btnGuardar_Click" />
            </div>

        </div>
    </div>

</asp:Content>