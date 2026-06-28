using CallCenterTPC.Datos;
using CallCenterTPC.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CallCenterTPC
{
    public partial class CerrarIncidencia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SeguridadHelper.HaySesion())
            {
                Response.Redirect("Login.aspx");
                return;
            }

            // Si entra por primera vez y NO trae un ID en la URL, lo pateamos al listado
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
                // Validación 1: Cumplir con la consigna del TP (comentario obligatorio)
                if (string.IsNullOrWhiteSpace(txtComentario.Text))
                {
                    AlertaHelper.MostrarAlerta(pnlMensaje, lblMensaje, "El comentario de resolución es obligatorio.", true);
                    return;
                }

                // Validación 2: Recuperar el ID de la URL
                if (Request.QueryString["id"] != null)
                {
                    int idIncidencia = int.Parse(Request.QueryString["id"]);
                    string comentario = txtComentario.Text.Trim();

                    IncidenciaRepositorio repo = new IncidenciaRepositorio();

                    // Llamamos al método que ya habíamos creado en el Repositorio
                    repo.CerrarIncidencia(idIncidencia, comentario);

                    AlertaHelper.GuardarMensajeExito("La incidencia fue cerrada correctamente.");

                    // TODO: (Punto 4) Aquí enviarás el mail al cliente avisando de la resolución

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