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
                e.Row.Cells[1].Text = obj.cliente.nombre; // Columna 1 = Cliente
                e.Row.Cells[2].Text = obj.tipoIncidencia.nombre; // Columna 2 = Tipo
                e.Row.Cells[3].Text = obj.prioridad.nombre; // Columna 3 = Prioridad
                e.Row.Cells[4].Text = obj.estado.nombre; // Columna 4 = Estado
            }
        }
    }
}