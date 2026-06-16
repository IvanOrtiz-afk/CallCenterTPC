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

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            LinkButton boton = (LinkButton)sender;

            int id = int.Parse(boton.CommandArgument);

            Usuario usuarioActual = SeguridadHelper.UsuarioActual();

            if (usuarioActual.id == id)
            {
                AlertaHelper.MostrarAlerta(
                    pnlMensaje,
                    lblMensaje,
                    "No puedes darte de baja a tí mismo.", //sino un Admin podria darse de baja a el mismo (Buena practica)
                    true);

                return;
            }

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