using CallCenterTPC.Datos;
using CallCenterTPC.Dominio;
using CallCenterTPC.Utilidades;
using System;

namespace CallCenterTPC
{
    public partial class DetalleIncidencia : System.Web.UI.Page
    {

        private IncidenciaRepositorio repo = new IncidenciaRepositorio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SeguridadHelper.HaySesion())
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("Incidencias.aspx");
                return;
            }

            if (!IsPostBack)
            {
                int id = int.Parse(Request.QueryString["id"]);

                Incidencia incidencia = repo.ObtenerPorId(id);

                if (incidencia == null)
                {
                    Response.Redirect("Incidencias.aspx");
                    return;
                }

                lblId.Text = incidencia.id.ToString();

                txtDescripcion.Text = incidencia.descripcion;

                lblFechaAlta.Text = incidencia.fechaAlta.ToString("dd/MM/yyyy");

                lblCliente.Text = incidencia.cliente.nombre;
                lblTipo.Text = incidencia.tipoIncidencia.nombre;
                lblPrioridad.Text = incidencia.prioridad.nombre;
                lblEstado.Text = incidencia.estado.nombre;

                lblUsuarioCreador.Text = incidencia.usuarioCreador.nombre;

                lblUsuarioAsignado.Text =
                    incidencia.usuarioAsignado != null
                        ? incidencia.usuarioAsignado.nombre
                        : "Sin asignar";

                if (SeguridadHelper.TienePermiso(Permisos.ReasignarIncidencias))
                {
                    pnlAsignar.Visible = true;

                    UsuarioRepositorio repoUsuarios = new UsuarioRepositorio();

                    ddlAgentes.DataSource = repoUsuarios.ListarAgentes();
                    ddlAgentes.DataTextField = "NombreCompleto";
                    ddlAgentes.DataValueField = "id";
                    ddlAgentes.DataBind();
                }

                if (incidencia.estadoId == 3 || incidencia.estadoId == 6)
                {
                    btnReabrir.Visible = true;
                }
                else
                {
                    btnReabrir.Visible = false;
                }
            }
        }

        protected void btnAsignar_Click(object sender, EventArgs e)
        {
            try
            {
                int idIncidencia = int.Parse(Request.QueryString["id"]);
                int idAgente = int.Parse(ddlAgentes.SelectedValue);

                repo.ActualizarAsignacion(idIncidencia, idAgente);

                AlertaHelper.GuardarMensajeExito("La incidencia fue asignada correctamente.");

                Response.Redirect("DetalleIncidencia.aspx?id=" + idIncidencia, false);
            }
            catch (Exception ex)
            {
                AlertaHelper.MostrarAlerta(
                    pnlMensaje,
                    lblMensaje,
                    ex.Message,
                    true);
            }
        }

        protected void btnReabrir_Click(object sender, EventArgs e)
        {
            try
            {
                int idIncidencia = int.Parse(Request.QueryString["id"]);

                // Ejecutamos el cambio de estado en la DB
                repo.ReabrirIncidencia(idIncidencia);

                AlertaHelper.GuardarMensajeExito("La incidencia ha sido reabierta con éxito.");

                // Redireccionamos a la misma página para refrescar los datos y volver a evaluar los botones
                Response.Redirect("DetalleIncidencia.aspx?id=" + idIncidencia, false);
            }
            catch (Exception ex)
            {
                AlertaHelper.MostrarAlerta(pnlMensaje, lblMensaje, ex.Message, true);
            }
        }
    }
}