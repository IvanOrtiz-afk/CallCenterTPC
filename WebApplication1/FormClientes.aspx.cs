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

            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    lblTitulo.Text = "Modificar Cliente";
                    btnEliminar.Visible = true;

                    int id = int.Parse(Request.QueryString["id"]);
                    ClienteRepositorio repo = new ClienteRepositorio();

                    Cliente clienteSeleccionado = repo.Listar().Find(x => x.id == id);

                    if (clienteSeleccionado != null)
                    {
                        txtNombre.Text = clienteSeleccionado.nombre;
                        txtApellido.Text = clienteSeleccionado.apellido;
                        txtDocumento.Text = clienteSeleccionado.documento.ToString();
                        txtTelefono.Text = clienteSeleccionado.telefono.ToString();
                        txtEmail.Text = clienteSeleccionado.email;

                   
                        if (clienteSeleccionado.activo)
                        {
                          
                            btnEliminar.Text = "Dar de baja";
                            btnEliminar.CssClass = "btn btn-danger";

                            btnEliminar.OnClientClick = "return confirm('¿Está seguro que desea dar de baja a este cliente?');";

                            btnGuardar.Visible = true;
                            txtNombre.Enabled = true;
                            txtApellido.Enabled = true;
                            txtDocumento.Enabled = true;
                            txtTelefono.Enabled = true;
                            txtEmail.Enabled = true;
                        }
                        else
                        {
                         
                            btnEliminar.Text = "Reactivar cliente";
                            btnEliminar.CssClass = "btn btn-success";

                            btnEliminar.OnClientClick = "return confirm('¿Está seguro que desea reactivar a este cliente?');";

                            btnGuardar.Visible = false;
                            txtNombre.Enabled = false;
                            txtApellido.Enabled = false;
                            txtDocumento.Enabled = false;
                            txtTelefono.Enabled = false;
                            txtEmail.Enabled = false;
                        }
                    }
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                pnlError.Visible = false;

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

                ClienteRepositorio repo = new ClienteRepositorio();

                // si hay un ID en la URL, estamos modificando. Si no, agregando.
                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    Cliente clienteOriginal = repo.Listar().Find(x => x.id == id);

                    if (txtEmail.Text != clienteOriginal.email && repo.ExisteEmail(txtEmail.Text))
                    {
                        lblError.Text = "Ya existe otro cliente con ese email.";
                        pnlError.Visible = true;
                        return;
                    }

                    if (documento != clienteOriginal.documento && repo.ExisteDocumento(documento))
                    {
                        lblError.Text = "Ya existe otro cliente con ese documento.";
                        pnlError.Visible = true;
                        return;
                    }

                    Cliente clienteModificado = new Cliente();
                    clienteModificado.id = id;
                    clienteModificado.nombre = txtNombre.Text;
                    clienteModificado.apellido = txtApellido.Text;
                    clienteModificado.email = txtEmail.Text;
                    clienteModificado.documento = documento;
                    clienteModificado.telefono = telefono;

                    
                    clienteModificado.activo = clienteOriginal.activo;

                    repo.Actualizar(clienteModificado);

                    Session["MensajeExito"] = $"¡El cliente {clienteModificado.nombre} {clienteModificado.apellido} se actualizó correctamente!";
                }
                else
                {
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

                    // Un cliente nuevo siempre nace activo
                    nuevoCliente.activo = true;

                    repo.Agregar(nuevoCliente);

                    Session["MensajeExito"] = $"¡El cliente {nuevoCliente.nombre} {nuevoCliente.apellido} se registró correctamente!";
                }

                Response.Redirect("Clientes.aspx", false);
            }
            catch (Exception ex)
            {
                pnlError.Visible = true;
                lblError.Text = "Ocurrió un problema al guardar: " + ex.Message;
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    ClienteRepositorio repo = new ClienteRepositorio();

                   
                    Cliente clienteTarget = repo.Listar().Find(x => x.id == id);

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
            catch (Exception ex)
            {
                pnlError.Visible = true;
                lblError.Text = "Ocurrió un problema al cambiar el estado: " + ex.Message;
            }
        }
    }
}