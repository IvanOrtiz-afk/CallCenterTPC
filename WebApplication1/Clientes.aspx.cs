using System;
using CallCenterTPC.Utilidades;
using CallCenterTPC.Datos;

namespace CallCenterTPC
{
    public partial class Clientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Mostrar mensaje de éxito si venimos de registrar uno
            AlertaHelper.CargarMensajeRedirigido(pnlMensaje, lblMensaje);

            if (!IsPostBack)
            {
                dgvClientes.DataSource = new ClienteRepositorio().Listar();
                dgvClientes.DataBind();
            }
        }
    }
}