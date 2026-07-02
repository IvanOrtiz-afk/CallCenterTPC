using CallCenterTPC.Datos;
using CallCenterTPC.Dominio;
using CallCenterTPC.Utilidades;
using System;
using System.Web.UI.WebControls;

namespace CallCenterTPC
{
    public partial class Usuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SeguridadHelper.EsAdmin())
            {
                Response.Redirect("Default.aspx");
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
            UsuarioRepositorio repo = new UsuarioRepositorio();
            string filtro = txtBuscar.Text.Trim();

            if (!string.IsNullOrEmpty(filtro))
            {
                dgvUsuarios.DataSource = repo.Buscar(filtro);
            }
            else if (chkVerInactivos.Checked)
            {
                dgvUsuarios.DataSource = repo.Listar();
            }
            else
            {
                dgvUsuarios.DataSource = repo.ListarActivos();
            }

            dgvUsuarios.DataBind();
        }

        protected void dgvUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvUsuarios.PageIndex = e.NewPageIndex;
            CargarGrilla();
        }

        protected void chkVerInactivos_CheckedChanged(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            CargarGrilla();
        }

        protected void lnkConfirmarCambioEstado_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(hfIdSeleccionado.Value);

            Usuario usuarioActual = SeguridadHelper.UsuarioActual();

            if (usuarioActual.id == id)
            {
                AlertaHelper.MostrarAlerta(pnlMensaje, lblMensaje, "No puedes darte de baja a tí mismo.", true);
                return;
            }

            UsuarioRepositorio repo = new UsuarioRepositorio();
            Usuario usuarioTarget = repo.ObtenerPorId(id);

            if (usuarioTarget != null)
            {
                bool nuevoEstado = !usuarioTarget.activo;
                repo.CambiarEstado(id, nuevoEstado);

                string accion = nuevoEstado ? "reactivado" : "dado de baja";
                Session["MensajeExito"] = $"¡El usuario fue {accion} correctamente!";

                Response.Redirect("Usuarios.aspx", false);
            }
        }
    }
}