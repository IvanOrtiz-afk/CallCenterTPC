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
    public partial class Prioridades : System.Web.UI.Page
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
            AlertaHelper.CargarMensajeRedirigido(pnlMensaje, lblMensaje);

            if (!IsPostBack)
            {
                dgvPrioridades.DataSource = new PrioridadRepositorio().Listar();
                dgvPrioridades.DataBind();
            }

        }
    }
}