using CallCenterTPC.Datos;
using CallCenterTPC.Utilidades;
using System;


namespace CallCenterTPC
{
    public partial class ResolverIncidencia : System.Web.UI.Page
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
                if (Request.QueryString["id"] == null)
                {
                    Response.Redirect("Incidencias.aspx");
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (string.IsNullOrWhiteSpace(txtComentario.Text))
                {
                    AlertaHelper.MostrarAlerta(pnlMensaje, lblMensaje, "El comentario de resolución es obligatorio.", true);
                    return;
                }

               
                if (Request.QueryString["id"] != null)
                {
                    int idIncidencia = int.Parse(Request.QueryString["id"]);
                    string comentario = txtComentario.Text.Trim();

                    IncidenciaRepositorio repo = new IncidenciaRepositorio();

                   
                    repo.ResolverIncidencia(idIncidencia, comentario);

                  
                    /*string emailCliente = repo.ObtenerEmailClienteDeIncidencia(idIncidencia);
                    EmailService emailService = new EmailService();
                    emailService.ArmarCorreo(emailCliente, "Incidencia Resuelta", "...");
                    emailService.EnviarEmail();*/

                    AlertaHelper.GuardarMensajeExito("La incidencia fue resuelta correctamente.");

                    Response.Redirect("Incidencias.aspx", false);
                }
            }
            catch (Exception ex)
            {
                AlertaHelper.MostrarAlerta(pnlMensaje, lblMensaje, "Ocurrió un error: " + ex.Message, true);
            }
        }
    }
}