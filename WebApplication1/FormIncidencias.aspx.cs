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
            // El IsPostBack evita que los desplegables se recarguen de la BD al hacer click en Guardar
            if (!IsPostBack)
            {
                CargarDesplegables();
            }
        }

        private void CargarDesplegables()
        {
            try
            {
                // arrancamos los repos
                TipoIncidenciaRepositorio tipoRepo = new TipoIncidenciaRepositorio();
                EstadoIncidenciaRepositorio estadoRepo = new EstadoIncidenciaRepositorio();
                PrioridadRepositorio prioridadRepo = new PrioridadRepositorio();
                ClienteRepositorio clienteRepo = new ClienteRepositorio();

                // tipos
                ddlTipos.DataSource = tipoRepo.Listar();
                ddlTipos.DataTextField = "nombre";
                ddlTipos.DataValueField = "id";
                ddlTipos.DataBind();

                // estados
                ddlEstados.DataSource = estadoRepo.Listar();
                ddlEstados.DataTextField = "nombre";
                ddlEstados.DataValueField = "id";
                ddlEstados.DataBind();

                // prioridades
                ddlPrioridades.DataSource = prioridadRepo.Listar();
                ddlPrioridades.DataTextField = "nombre";
                ddlPrioridades.DataValueField = "id";
                ddlPrioridades.DataBind();

                // clientes (Acá aplicamos los cambios)
                ddlClientes.DataSource = clienteRepo.ObtenerTodos();
                ddlClientes.DataTextField = "infoDesplegable";
                ddlClientes.DataValueField = "id";
                ddlClientes.DataBind();
            }
            catch (Exception ex)
            {
                // si salta un error lo notifica
                AlertaHelper.MostrarAlerta(pnlMensaje, lblMensaje, "Error al cargar los datos: " + ex.Message, true);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
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
                nuevaIncidencia.estadoId = int.Parse(ddlEstados.SelectedValue);

                nuevaIncidencia.usuarioCreadorId = usuario.id;
                nuevaIncidencia.usuarioAsignadoId = usuario.id;

                nuevaIncidencia.descripcion = txtDescripcion.Text;

                new IncidenciaRepositorio().Agregar(nuevaIncidencia);

                AlertaHelper.GuardarMensajeExito(
                    "Incidencia creada con éxito.");

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