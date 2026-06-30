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

           
                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    lblTitulo.Text = "Editar Incidencia #" + id;
                    btnGuardar.Text = "Actualizar Incidencia"; 
                    CargarIncidenciaParaEdicion(id);
                }
                else
                {
                    lblTitulo.Text = "Registrar Nueva Incidencia";
                    btnGuardar.Text = "Guardar Incidencia";
                }
            }
        }

        private void CargarDesplegables()
        {
            try
            {
                TipoIncidenciaRepositorio tipoRepo = new TipoIncidenciaRepositorio();
                PrioridadRepositorio prioridadRepo = new PrioridadRepositorio();
                ClienteRepositorio clienteRepo = new ClienteRepositorio();

           
                ddlTipos.DataSource = tipoRepo.Listar();
                ddlTipos.DataTextField = "nombre";
                ddlTipos.DataValueField = "id";
                ddlTipos.DataBind();

            
                ddlPrioridades.DataSource = prioridadRepo.Listar();
                ddlPrioridades.DataTextField = "nombre";
                ddlPrioridades.DataValueField = "id";
                ddlPrioridades.DataBind();

             
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

        private void CargarIncidenciaParaEdicion(int id)
        {
            try
            {
                IncidenciaRepositorio repo = new IncidenciaRepositorio();

               
                Incidencia inc = repo.ObtenerPorId(id);

                if (inc != null)
                {
                    txtAsunto.Text = inc.asunto;
                    txtDescripcion.Text = inc.descripcion;

                    ddlClientes.SelectedValue = inc.clienteId.ToString();
                    ddlTipos.SelectedValue = inc.tipoIncidenciaId.ToString();
                    ddlPrioridades.SelectedValue = inc.prioridadId.ToString();
                }
            }
            catch (Exception ex)
            {
                AlertaHelper.MostrarAlerta(pnlMensaje, lblMensaje, "Error al cargar la incidencia: " + ex.Message, true);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (string.IsNullOrWhiteSpace(txtAsunto.Text))
                {
                    AlertaHelper.MostrarAlerta(pnlMensaje, lblMensaje, "Debe ingresar un asunto.", true);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    AlertaHelper.MostrarAlerta(pnlMensaje, lblMensaje, "Debe ingresar una descripción.", true);
                    return;
                }

                Usuario usuario = SeguridadHelper.UsuarioActual();
                IncidenciaRepositorio repo = new IncidenciaRepositorio();
                Incidencia incidenciaActual = new Incidencia();
                ClienteRepositorio clienteRepo = new ClienteRepositorio();

              
                incidenciaActual.clienteId = int.Parse(ddlClientes.SelectedValue);
                incidenciaActual.tipoIncidenciaId = int.Parse(ddlTipos.SelectedValue);
                incidenciaActual.prioridadId = int.Parse(ddlPrioridades.SelectedValue);
                incidenciaActual.asunto = txtAsunto.Text.Trim();
                incidenciaActual.descripcion = txtDescripcion.Text.Trim();

               
                if (Request.QueryString["id"] != null)
                {
                 
                    incidenciaActual.id = int.Parse(Request.QueryString["id"]);

                    incidenciaActual.usuarioAsignadoId = usuario.id;
                    incidenciaActual.estadoId = 2; 

                   
                    repo.Modificar(incidenciaActual); 

                    AlertaHelper.GuardarMensajeExito("La incidencia fue actualizada y pasó al estado 'En Análisis'.");
                }
                else
                {
                   
                    incidenciaActual.estadoId = 1; 
                    incidenciaActual.usuarioCreadorId = usuario.id;
                    incidenciaActual.usuarioAsignadoId = usuario.id; 

                    repo.Agregar(incidenciaActual);

                    string emailCliente = clienteRepo.ObtenerEmailPorId(int.Parse(ddlClientes.SelectedValue));

                   /* EmailService emailService = new EmailService();
                    emailService.ArmarCorreo(emailCliente,
                        "Nueva Incidencia Registrada - ID: " + incidenciaActual.id,
                        "<h1>Hola!</h1><p>Se ha registrado su incidencia número <b>" + incidenciaActual.id + "</b>.</p><p>Detalle: " + incidenciaActual.descripcion + "</p>");
                    emailService.EnviarEmail();*/

                    AlertaHelper.GuardarMensajeExito("La incidencia fue creada correctamente.");
                    
                  
                }

                Response.Redirect("Incidencias.aspx", false);
            }
            catch (Exception ex)
            {
                AlertaHelper.MostrarAlerta(pnlMensaje, lblMensaje, ex.Message, true);
            }
        }
    }
    
}