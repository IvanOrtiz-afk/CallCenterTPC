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
                   
                    pnlError.Visible = false;

              
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    AlertaHelper.MostrarAlerta(pnlMensaje,lblMensaje,"Debe ingresar un nombre.",true);

                    return;
                }

               
                TipoIncidencia nuevoTipo = new TipoIncidencia();
                    TipoIncidenciaRepositorio repo = new TipoIncidenciaRepositorio();

                   
                    nuevoTipo.nombre = txtNombre.Text;

                 
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