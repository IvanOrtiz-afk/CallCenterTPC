using CallCenterTPC.Datos;
using CallCenterTPC.Utilidades;
using CallCenterTPC.Dominio;
using System;

namespace CallCenterTPC
{
    public partial class FormClientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SeguridadHelper.HaySesion())
            {
                Response.Redirect("Login.aspx");
                return;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                pnlError.Visible = false;

                // validaciones

                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    lblError.Text = "Debe ingresar un nombre.";
                    pnlError.Visible = true;
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtApellido.Text))
                {
                    lblError.Text = "Debe ingresar un apellido.";
                    pnlError.Visible = true;
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    lblError.Text = "Debe ingresar un email.";
                    pnlError.Visible = true;
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtDocumento.Text))
                {
                    lblError.Text = "Debe ingresar un documento.";
                    pnlError.Visible = true;
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtTelefono.Text))
                {
                    lblError.Text = "Debe ingresar un teléfono.";
                    pnlError.Visible = true;
                    return;
                }

                int documento;

                if (!int.TryParse(txtDocumento.Text, out documento))
                {
                    lblError.Text = "El documento debe contener solamente números.";
                    pnlError.Visible = true;
                    return;
                }

                int telefono;

                if (!int.TryParse(txtTelefono.Text, out telefono))
                {
                    lblError.Text = "El teléfono debe contener solamente números.";
                    pnlError.Visible = true;
                    return;
                }

                // se crea el cliente

                ClienteRepositorio repo = new ClienteRepositorio();

                if (repo.ExisteEmail(txtEmail.Text))
                {
                    lblError.Text = "Ya existe un cliente con ese email.";
                    pnlError.Visible = true;
                    return;
                }

                if (repo.ExisteDocumento(documento))
                {
                    lblError.Text = "Ya existe un cliente con ese documento.";
                    pnlError.Visible = true;
                    return;
                }

                Cliente nuevoCliente = new Cliente();

                nuevoCliente.nombre = txtNombre.Text;
                nuevoCliente.apellido = txtApellido.Text;
                nuevoCliente.email = txtEmail.Text;
                nuevoCliente.documento = documento;
                nuevoCliente.telefono = telefono;
                nuevoCliente.activo = chkActivo.Checked;

                repo.Agregar(nuevoCliente);

                Session["MensajeExito"] =
                    $"¡El cliente {nuevoCliente.nombre} {nuevoCliente.apellido} se registró correctamente!";

                Response.Redirect("Clientes.aspx", false);
            }
            catch (Exception ex)
            {
                pnlError.Visible = true;
                lblError.Text = "Ocurrió un problema al guardar: " + ex.Message;
            }
        }
    }
}