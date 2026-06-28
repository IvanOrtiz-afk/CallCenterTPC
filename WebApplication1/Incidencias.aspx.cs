using CallCenterTPC.Datos;
using CallCenterTPC.Dominio;
using CallCenterTPC.Utilidades;
using System;
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
        
            AlertaHelper.CargarMensajeRedirigido(pnlMensaje, lblMensaje);

            if (!IsPostBack)
            {
                CargarGrilla();
            }
        }

        private void CargarGrilla()
        {
            try
            {
               
                Usuario usuario = SeguridadHelper.UsuarioActual();

                IncidenciaRepositorio repo = new IncidenciaRepositorio();

            
                dgvIncidencias.DataSource = repo.Listar(usuario.rolId, usuario.id);
                dgvIncidencias.DataBind();
            }
            catch (Exception ex)
            {
                AlertaHelper.MostrarAlerta(pnlMensaje, lblMensaje, "Error al cargar las incidencias: " + ex.Message, true);
            }
        }

        protected void dgvIncidencias_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               
                string estadoActual = DataBinder.Eval(e.Row.DataItem, "estado.nombre").ToString();

                
                HyperLink lnkEditar = (HyperLink)e.Row.FindControl("lnkEditar");
                HyperLink lnkResolver = (HyperLink)e.Row.FindControl("lnkResolver");
                HyperLink lnkCerrar = (HyperLink)e.Row.FindControl("lnkCerrar");

               
                switch (estadoActual)
                {
                    case "Cerrado":
                    case "Resuelto":
                        lnkEditar.Visible = false;
                        lnkResolver.Visible = false;
                        lnkCerrar.Visible = false;
                        break;

                    case "Abierto":
                    case "Asignado":
                    case "Reabierto":
                    case "En Análisis": 
                        break;
                }
            }
        }
    }
}