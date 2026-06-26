using CallCenterTPC.Datos;
using CallCenterTPC.Dominio;
using CallCenterTPC.Utilidades;
using System;

namespace CallCenterTPC
{
    public partial class FormIncidencias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SeguridadHelper.HaySesion())
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarDesplegables();
            }
        }

        private void CargarDesplegables()
        {
            try
            {
                TipoIncidenciaRepositorio tipoRepo = new TipoIncidenciaRepositorio();
                PrioridadRepositorio prioridadRepo = new PrioridadRepositorio();
                ClienteRepositorio clienteRepo = new ClienteRepositorio();

                // tipos de incidencia
                ddlTipos.DataSource = tipoRepo.Listar();
                ddlTipos.DataTextField = "nombre";
                ddlTipos.DataValueField = "id";
                ddlTipos.DataBind();

                //prioridades
                ddlPrioridades.DataSource = prioridadRepo.Listar();
                ddlPrioridades.DataTextField = "nombre";
                ddlPrioridades.DataValueField = "id";
                ddlPrioridades.DataBind();

                // clientes
                ddlClientes.DataSource = clienteRepo.ObtenerTodos();
                ddlClientes.DataTextField = "infoDesplegable";
                ddlClientes.DataValueField = "id";
                ddlClientes.DataBind();
            }
            catch (Exception ex)
            {
                AlertaHelper.MostrarAlerta(
                    pnlMensaje,
                    lblMensaje,
                    "Error al cargar los datos: " + ex.Message,
                    true);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // validaciones

                if (string.IsNullOrWhiteSpace(txtAsunto.Text))
                {
                    AlertaHelper.MostrarAlerta(
                        pnlMensaje,
                        lblMensaje,
                        "Debe ingresar un asunto.",
                        true);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    AlertaHelper.MostrarAlerta(
                        pnlMensaje,
                        lblMensaje,
                        "Debe ingresar una descripción.",
                        true);
                    return;
                }

                Usuario usuario = SeguridadHelper.UsuarioActual();

                Incidencia nuevaIncidencia = new Incidencia();

                nuevaIncidencia.clienteId = int.Parse(ddlClientes.SelectedValue);
                nuevaIncidencia.tipoIncidenciaId = int.Parse(ddlTipos.SelectedValue);
                nuevaIncidencia.prioridadId = int.Parse(ddlPrioridades.SelectedValue);

                // incidencia nace ABIERTA
                nuevaIncidencia.estadoId = 1;

                // user que crea la incidencia
                nuevaIncidencia.usuarioCreadorId = usuario.id;

                // cambiar a NULL
                // cuando adapte la DB
                nuevaIncidencia.usuarioAsignadoId = null;

                nuevaIncidencia.asunto = txtAsunto.Text.Trim();
                nuevaIncidencia.descripcion = txtDescripcion.Text.Trim();

                IncidenciaRepositorio repo = new IncidenciaRepositorio();
                repo.Agregar(nuevaIncidencia);

                AlertaHelper.GuardarMensajeExito(
                    "La incidencia fue creada correctamente.");

                Response.Redirect("Incidencias.aspx", false);
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
    }
}