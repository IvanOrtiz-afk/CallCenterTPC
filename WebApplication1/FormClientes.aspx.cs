using CallCenterTPC.Datos;
using System;


namespace CallCenterTPC
{
    public partial class FormClientes : System.Web.UI.Page
    {
       

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Ocultamos el panel de error al intentar guardar nuevamente
                pnlError.Visible = false;

                CallCenterTPC.Dominio.Cliente nuevoCliente = new CallCenterTPC.Dominio.Cliente();
                ClienteRepositorio repo = new ClienteRepositorio();

                nuevoCliente.nombre = txtNombre.Text;
                nuevoCliente.apellido = txtApellido.Text;
                nuevoCliente.email = txtEmail.Text;

                nuevoCliente.documento = int.Parse(txtDocumento.Text);
                nuevoCliente.telefono = int.Parse(txtTelefono.Text);
                nuevoCliente.activo = chkActivo.Checked;

                repo.Agregar(nuevoCliente);

                // 2. Guardamos el mensaje de éxito en la memoria temporal (Session)
                Session["MensajeExito"] = $"¡El cliente {nuevoCliente.nombre} {nuevoCliente.apellido} se registró correctamente!";

                // 3. Redirigimos al inicio
                Response.Redirect("Clientes.aspx", false);
            }
            catch (Exception ex)
            {
                // 4. Si hay un error, NO redirige. Hacemos visible el panel rojo y mostramos el error.
                pnlError.Visible = true;

                // Si es un error de formato (ej: dejó vacío un número), le damos un mensaje más amigable
                if (ex is FormatException)
                {
                    lblError.Text = "Por favor, verifique que los campos de Documento y Teléfono contengan solo números.";
                }
                else
                {
                    lblError.Text = "Ocurrió un problema al guardar: " + ex.Message;
                }
            }
        }
    }
}