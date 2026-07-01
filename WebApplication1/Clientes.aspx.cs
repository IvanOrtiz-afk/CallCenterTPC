using CallCenterTPC.Datos;
using CallCenterTPC.Utilidades;
using System;
using System.Web.UI.WebControls;

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
               
                dgvClientes.DataSource = repo.Listar();
            }
            else
            {
                
                dgvClientes.DataSource = repo.ObtenerTodos();
            }

            dgvClientes.DataBind();
        }

        protected void dgvClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
           
            dgvClientes.PageIndex = e.NewPageIndex;

          
            CargarGrilla();
        }


        protected void chkVerInactivos_CheckedChanged(object sender, EventArgs e)
        {
            CargarGrilla();
        }
    }
}