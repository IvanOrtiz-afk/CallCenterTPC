using System;
using CallCenterTPC.Utilidades;
using CallCenterTPC.Datos;

namespace CallCenterTPC
{
    public partial class Clientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SeguridadHelper.HaySesion())
            {
                Response.Redirect("Login.aspx");
                return;
            }

            AlertaHelper.CargarMensajeRedirigido(pnlMensaje, lblMensaje);

            if (!IsPostBack)
            {
                dgvClientes.DataSource = new ClienteRepositorio().ObtenerTodos();
                dgvClientes.DataBind();
            }
        }
    }
}