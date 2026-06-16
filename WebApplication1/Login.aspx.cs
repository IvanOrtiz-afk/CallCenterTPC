using System;
using System.Web.UI;
using CallCenterTPC.Datos;
using CallCenterTPC.Dominio;
using CallCenterTPC.Utilidades;

namespace CallCenterTPC
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SeguridadHelper.HaySesion())
            {
                Response.Redirect("Default.aspx");
                return;
            }
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validaciones

                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    lblError.Text = "Debe ingresar un email";
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    lblError.Text = "Debe ingresar una contraseña";
                    return;
                }

                UsuarioRepositorio repo = new UsuarioRepositorio();

                Usuario usuario = repo.ObtenerPorEmail(txtEmail.Text);

                if (usuario == null)
                {
                    lblError.Text = "Usuario inexistente";
                    return;
                }

                if (!usuario.activo)
                {
                    lblError.Text = "Usuario inactivo";
                    return;
                }

                if (usuario.password != txtPassword.Text)
                {
                    lblError.Text = "Contraseña incorrecta";
                    return;
                }

                Session["Usuario"] = usuario;

                Response.Redirect("Default.aspx");
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
    }
}