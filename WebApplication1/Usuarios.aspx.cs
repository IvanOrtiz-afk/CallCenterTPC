using CallCenterTPC.Datos;
using CallCenterTPC.Utilidades;
using System;

namespace CallCenterTPC
{
    public partial class Usuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AlertaHelper.CargarMensajeRedirigido(pnlMensaje, lblMensaje);

            if (!IsPostBack)
            {
                CargarGrilla();
            }
        }

        private void CargarGrilla()
        {
            // Asumo que tu UsuarioRepositorio tiene un método Listar()
            dgvUsuarios.DataSource = new UsuarioRepositorio().Listar();
            dgvUsuarios.DataBind();
        }
    }
}