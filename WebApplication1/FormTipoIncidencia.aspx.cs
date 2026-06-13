using CallCenterTPC.Datos;
using CallCenterTPC.Utilidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebGrease.Activities;

namespace CallCenterTPC
{
    public partial class FormTipoIncidencia : System.Web.UI.Page
    {
            protected void Page_Load(object sender, EventArgs e)
            {
               
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
                        pnlError.Visible = true;
                        lblError.Text = "El nombre del tipo de incidencia no puede estar vacío.";
                        return; // Cortamos la ejecución aquí
                    }

                    // Instanciamos el modelo y el repositorio
                    CallCenterTPC.Dominio.TipoIncidencia nuevoTipo = new CallCenterTPC.Dominio.TipoIncidencia();
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