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
                // Instanciamos los repositorios
                // NOTA: Asumo que ya tienes un ClienteRepositorio y PrioridadRepositorio con el método Listar()
                TipoIncidenciaRepositorio tipoRepo = new TipoIncidenciaRepositorio();
                EstadoIncidenciaRepositorio estadoRepo = new EstadoIncidenciaRepositorio();
                PrioridadRepositorio prioridadRepo = new PrioridadRepositorio();
                ClienteRepositorio clienteRepo = new ClienteRepositorio(); 

                // --- Cargar Tipos ---
                ddlTipos.DataSource = tipoRepo.Listar();
                ddlTipos.DataTextField = "nombre"; // Lo que ve el usuario
                ddlTipos.DataValueField = "id";    // El ID que se guarda en la BD
                ddlTipos.DataBind();

                // --- Cargar Estados ---
                ddlEstados.DataSource = estadoRepo.Listar();
                ddlEstados.DataTextField = "nombre";
                ddlEstados.DataValueField = "id";
                ddlEstados.DataBind();

                // --- Cargar Prioridades ---
                ddlPrioridades.DataSource = prioridadRepo.Listar();
                ddlPrioridades.DataTextField = "nombre";
                ddlPrioridades.DataValueField = "id";
                ddlPrioridades.DataBind();

                // --- Cargar Clientes ---
                 ddlClientes.DataSource = clienteRepo.Listar();
                 ddlClientes.DataTextField = "nombre"; // Podrías armar una propiedad que devuelva Nombre + Apellido
                 ddlClientes.DataValueField = "id";
                 ddlClientes.DataBind();
            }
            catch (Exception ex)
            {
                // Manejar el error (ej: mostrar un mensaje en pantalla)
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Incidencia nuevaIncidencia = new Incidencia();
                IncidenciaRepositorio repo = new IncidenciaRepositorio();

                // Capturamos los valores de los DropDownList convirtiéndolos a enteros
                nuevaIncidencia.clienteId = int.Parse(ddlClientes.SelectedValue);
                nuevaIncidencia.tipoIncidenciaId = int.Parse(ddlTipos.SelectedValue);
                nuevaIncidencia.prioridadId = int.Parse(ddlPrioridades.SelectedValue);
                nuevaIncidencia.estadoId = int.Parse(ddlEstados.SelectedValue);

                // En un sistema real, este ID se tomaría de la sesión del usuario logueado.
                // Por ahora lo forzamos a 1 (o el ID que tengas en tu tabla usuarios).
                nuevaIncidencia.usuarioCreadorId = 1;
                nuevaIncidencia.usuarioAsignadoId = 1; // O 0/null si nace sin asignar

                nuevaIncidencia.descripcion = txtDescripcion.Text;

                // Llamamos al repositorio para que ejecute el INSERT
                repo.Agregar(nuevaIncidencia);


                AlertaHelper.GuardarMensajeExito("Incidencia creada con éxito.");
                Response.Redirect("Incidencias.aspx", false);
            }
            catch (Exception ex)
            {
                AlertaHelper.MostrarAlerta(pnlMensaje, lblMensaje, ex.Message, true);
            }
        }
    }
}