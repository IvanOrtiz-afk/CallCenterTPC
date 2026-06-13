using CallCenterTPC.Datos;
using CallCenterTPC.Dominio;
using CallCenterTPC.Utilidades;
using System;


namespace CallCenterTPC
{
    public partial class FormUsuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Cargamos los roles para evitar errores de FK
                ddlRoles.DataSource = new RolRepositorio().Listar();
                ddlRoles.DataTextField = "nombre";
                ddlRoles.DataValueField = "id";
                ddlRoles.DataBind();
            }
            AlertaHelper.CargarMensajeRedirigido(pnlMensaje, lblMensaje);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario nuevo = new Usuario();
                nuevo.nombre = txtNombre.Text;
                nuevo.apellido = txtApellido.Text;
                nuevo.email = txtEmail.Text;
                nuevo.password = txtPassword.Text; // Recuerda luego hashear esto
                nuevo.rolId = int.Parse(ddlRoles.SelectedValue);

                new UsuarioRepositorio().Agregar(nuevo);

                AlertaHelper.GuardarMensajeExito("Usuario creado correctamente.");
                Response.Redirect("Default.aspx", false);
            }
            catch (Exception ex)
            {
                AlertaHelper.MostrarAlerta(pnlMensaje, lblMensaje, ex.Message, true);
            }
        }
    }

}