using CallCenterTPC.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CallCenterTPC
{
    public partial class TiposIncidencia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Solo cargamos la grilla la primera vez que entra a la página, 
            // no cuando hace PostBack (por ejemplo, si hubiera botones adentro de la grilla)
            if (!IsPostBack)
            {
                CargarGrilla();
            }
        }

        private void CargarGrilla()
        {
            try
            {
                TipoIncidenciaRepositorio repo = new TipoIncidenciaRepositorio();

                // Le pasamos la lista de la base de datos al DataSource del GridView
                dgvTiposIncidencia.DataSource = repo.Listar();

                // DataBind() es el comando que le dice a .NET: "Dibuja la tabla en el HTML"
                dgvTiposIncidencia.DataBind();
            }
            catch (Exception ex)
            {
                // En un escenario real, podrías mostrar un cartel de error aquí
                // Por ejemplo, usando un panel similar al que armamos en la creación de clientes.
            }
        }
    }
}