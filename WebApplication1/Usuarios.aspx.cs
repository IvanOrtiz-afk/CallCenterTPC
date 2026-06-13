using CallCenterTPC.Datos;
using CallCenterTPC.Utilidades;
using System;
using System.Web.UI.WebControls;

namespace CallCenterTPC
{
    public partial class Usuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
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

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            LinkButton boton = (LinkButton)sender;

            int id = int.Parse(boton.CommandArgument);

            UsuarioRepositorio repo = new UsuarioRepositorio();

            repo.Eliminar(id);

            CargarGrilla();
        }

        private void CargarGrilla()
        {
            // Asumo que tu UsuarioRepositorio tiene un método Listar()
            dgvUsuarios.DataSource = new UsuarioRepositorio().Listar();
            dgvUsuarios.DataBind();
        }
    }
}