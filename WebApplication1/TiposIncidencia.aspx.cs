using CallCenterTPC.Datos;
using CallCenterTPC.Utilidades;
using System;

namespace CallCenterTPC
{
    public partial class TiposIncidencia : System.Web.UI.Page
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

            try
            {
                if (!IsPostBack)
                {
                    CargarGrilla();
                }
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

        private void CargarGrilla()
        {
            TipoIncidenciaRepositorio repo =
                new TipoIncidenciaRepositorio();

            dgvTiposIncidencia.DataSource = repo.Listar();
            dgvTiposIncidencia.DataBind();
        }
    }
}
