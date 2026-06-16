using CallCenterTPC.Datos;
using CallCenterTPC.Utilidades;
using System;
using CallCenterTPC.Dominio;

namespace CallCenterTPC
{
    public partial class FormTipoIncidencia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SeguridadHelper.HaySesion())
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (SeguridadHelper.EsAgente())
            {
                Response.Redirect("Default.aspx");
                return;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
            {
                try
                {
                    // Ocultamos el panel de error al intentar guardar
                    pnlError.Visible = false;

                // Validación simple: que no envíen el campo vacío
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    AlertaHelper.MostrarAlerta(pnlMensaje,lblMensaje,"Debe ingresar un nombre.",true);

                    return;
                }

                // Instanciamos el modelo y el repositorio
                TipoIncidencia nuevoTipo = new TipoIncidencia();
                    TipoIncidenciaRepositorio repo = new TipoIncidenciaRepositorio();

                    // Asignamos el valor
                    nuevoTipo.nombre = txtNombre.Text;

                    // Guardamos en la base de datos
                    repo.Agregar(nuevoTipo);

                    AlertaHelper.GuardarMensajeExito("¡El tipo de incidencia se creó correctamente!");
                    Response.Redirect("TiposIncidencia.aspx", false);
            }
                catch (Exception ex)
                {
                    AlertaHelper.MostrarAlerta(pnlMensaje, lblMensaje, ex.Message, true);
                }
            }
        }
    
}