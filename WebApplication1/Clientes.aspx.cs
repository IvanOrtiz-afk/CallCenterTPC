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
                CargarGrilla();
            }
        }

       
        private void CargarGrilla()
        {
            ClienteRepositorio repo = new ClienteRepositorio();

            if (chkVerInactivos != null && chkVerInactivos.Checked)
            {
                // Si el tilde está puesto, traemos TODO (activos e inactivos)
                dgvClientes.DataSource = repo.Listar();
            }
            else
            {
                // Por defecto, traemos solo los activos
                dgvClientes.DataSource = repo.ObtenerTodos();
            }

            dgvClientes.DataBind();
        }

        
        protected void chkVerInactivos_CheckedChanged(object sender, EventArgs e)
        {
            CargarGrilla();
        }
    }
}