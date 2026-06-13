using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CallCenterTPC.Datos;
using CallCenterTPC.Dominio;

namespace CallCenterTPC
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] != null)
                Response.Redirect("Default.aspx");
        }
        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
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