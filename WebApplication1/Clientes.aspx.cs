using System;
using System.Web.UI.WebControls;
using CallCenterTPC.Utilidades;
using CallCenterTPC.Datos;
using CallCenterTPC.Dominio;

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
            string filtro = txtBuscar.Text.Trim();

            if (!string.IsNullOrEmpty(filtro))
            {
                dgvClientes.DataSource = repo.Buscar(filtro, chkVerInactivos.Checked);
            }
            else if (chkVerInactivos != null && chkVerInactivos.Checked)
            {
                dgvClientes.DataSource = repo.Listar();
            }
            else
            {
                dgvClientes.DataSource = repo.ObtenerTodos();
            }

            dgvClientes.DataBind();
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
            int idCliente = Convert.ToInt32(hfIdSeleccionado.Value);

            ClienteRepositorio repo = new ClienteRepositorio();
            Cliente clienteTarget = repo.Listar().Find(x => x.id == idCliente);

            if (clienteTarget != null)
            {
                clienteTarget.activo = !clienteTarget.activo;
                repo.Actualizar(clienteTarget);

                string accion = clienteTarget.activo ? "reactivado" : "dado de baja";
                Session["MensajeExito"] = $"¡El cliente fue {accion} correctamente!";

                Response.Redirect("Clientes.aspx", false);
            }
        }
    }
}