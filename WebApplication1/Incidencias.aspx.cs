using CallCenterTPC.Datos;
using CallCenterTPC.Dominio;
using CallCenterTPC.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CallCenterTPC
{
    public partial class Incidencias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SeguridadHelper.HaySesion())
            {
                Response.Redirect("Login.aspx");
                return;
            }
            // Mostrar mensaje de éxito si venimos de registrar uno
            AlertaHelper.CargarMensajeRedirigido(pnlMensaje, lblMensaje);

            if (!IsPostBack)
            {
                dgvIncidencias.DataSource = new IncidenciaRepositorio().Listar();
                dgvIncidencias.DataBind();
            }
        }

        protected void dgvIncidencias_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Incidencia obj = (Incidencia)e.Row.DataItem;

                e.Row.Cells[1].Text = obj.asunto;
                e.Row.Cells[2].Text = obj.cliente.nombre;
                e.Row.Cells[3].Text = obj.tipoIncidencia.nombre;
                e.Row.Cells[4].Text = obj.prioridad.nombre;
                e.Row.Cells[5].Text = obj.estado.nombre;
            }
        }
    }
}