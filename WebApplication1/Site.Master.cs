using CallCenterTPC.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CallCenterTPC.Utilidades;

namespace CallCenterTPC
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!SeguridadHelper.HaySesion())
            {
                Response.Redirect("Login.aspx");
                return;
            }

            Usuario usuario = SeguridadHelper.UsuarioActual();
            string rol = "";

                switch (usuario.rolId)
                {
                    case 1:
                        rol = "Administrador";
                        break;

                    case 3:
                        rol = "Agente";
                        break;

                    case 4:
                        rol = "Coordinador";
                        break;
                }

                if (usuario.rolId == 3) // rol agente
                {
                    liUsuarios.Visible = false;
                    liPrioridades.Visible = false;
                    liTiposIncidencia.Visible = false;
                }
                else if (usuario.rolId == 4) // rol coord
                {
                    liUsuarios.Visible = false;
                }

                lblUsuario.Text =
                    usuario.nombre + " " +
                    usuario.apellido +
                    " (" + rol + ")";
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();

            Response.Redirect("Login.aspx");
        }
    }
}