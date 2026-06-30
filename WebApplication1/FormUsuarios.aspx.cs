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
            if (!SeguridadHelper.EsAdmin())
            {
                Response.Redirect("Default.aspx");
                return;
            }

            if (!IsPostBack)
            {
                ddlRoles.DataSource = new RolRepositorio().Listar();
                ddlRoles.DataTextField = "nombre";
                ddlRoles.DataValueField = "id";
                ddlRoles.DataBind();

                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);

                    UsuarioRepositorio repo = new UsuarioRepositorio();

                    Usuario usuario = repo.ObtenerPorId(id);

                    if (usuario != null)
                    {
                        txtNombre.Text = usuario.nombre;
                        txtApellido.Text = usuario.apellido;
                        txtEmail.Text = usuario.email;
                        txtPassword.Text = usuario.password; //recordar hashear esto

                        ddlRoles.SelectedValue = usuario.rolId.ToString();
                        lblTitulo.Text = "Modificar Usuario";
                        btnGuardar.Text = "Modificar Usuario";
                    }
                }
            }

            AlertaHelper.CargarMensajeRedirigido(pnlMensaje, lblMensaje);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
              

                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    AlertaHelper.MostrarAlerta(
                        pnlMensaje,
                        lblMensaje,
                        "Debe ingresar un nombre.",
                        true);

                    return;
                }

                if (string.IsNullOrWhiteSpace(txtApellido.Text))
                {
                    AlertaHelper.MostrarAlerta(
                        pnlMensaje,
                        lblMensaje,
                        "Debe ingresar un apellido.",
                        true);

                    return;
                }

                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    AlertaHelper.MostrarAlerta(
                        pnlMensaje,
                        lblMensaje,
                        "Debe ingresar un email.",
                        true);

                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    AlertaHelper.MostrarAlerta(
                        pnlMensaje,
                        lblMensaje,
                        "Debe ingresar una contraseña.",
                        true);

                    return;
                }

                UsuarioRepositorio repo = new UsuarioRepositorio();

                if (Request.QueryString["id"] == null && repo.ExisteEmail(txtEmail.Text))
                {
                    AlertaHelper.MostrarAlerta(
                        pnlMensaje,
                        lblMensaje,
                        "Ya existe un usuario con ese email.",
                        true);

                    return;
                }

                Usuario nuevo = new Usuario();

                nuevo.nombre = txtNombre.Text;
                nuevo.apellido = txtApellido.Text;
                nuevo.email = txtEmail.Text;
                nuevo.password = txtPassword.Text;
                nuevo.rolId = int.Parse(ddlRoles.SelectedValue);

              

                if (Request.QueryString["id"] != null)
                {
                    nuevo.id = int.Parse(Request.QueryString["id"]);
                    nuevo.activo = true;

                    repo.Modificar(nuevo);

                    AlertaHelper.GuardarMensajeExito(
                        "Usuario modificado correctamente.");
                }
                else
                {
                    repo.Agregar(nuevo);

                    AlertaHelper.GuardarMensajeExito(
                        "Usuario creado correctamente.");
                }

                Response.Redirect("Usuarios.aspx", false);
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
    }
}