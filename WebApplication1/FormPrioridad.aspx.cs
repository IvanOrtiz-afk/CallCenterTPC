using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CallCenterTPC.Utilidades;
using CallCenterTPC.Datos;

namespace CallCenterTPC
{
    public partial class FormPrioridad : System.Web.UI.Page
    {
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                    throw new Exception("El nombre es obligatorio.");

               Dominio.Prioridad nueva = new CallCenterTPC.Dominio.Prioridad { nombre = txtNombre.Text };
                new PrioridadRepositorio().Agregar(nueva);

                AlertaHelper.GuardarMensajeExito("Prioridad creada con éxito.");
                Response.Redirect("Prioridades.aspx", false);
            }
            catch (Exception ex)
            {
                AlertaHelper.MostrarAlerta(pnlMensaje, lblMensaje, ex.Message, true);
            }
        }
    }
}