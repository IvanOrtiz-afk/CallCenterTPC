using System;
using CallCenterTPC.Utilidades;
using CallCenterTPC.Datos;
using CallCenterTPC.Dominio;

namespace CallCenterTPC
{
    public partial class FormPrioridad : System.Web.UI.Page
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
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    AlertaHelper.MostrarAlerta(
                        pnlMensaje,
                        lblMensaje,
                        "Debe ingresar un nombre.",
                        true);

                    return;
                }

                Prioridad nueva = new Prioridad();
                nueva.nombre = txtNombre.Text;

                new PrioridadRepositorio().Agregar(nueva);

                AlertaHelper.GuardarMensajeExito(
                    "Prioridad creada con éxito.");

                Response.Redirect("Prioridades.aspx", false);
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