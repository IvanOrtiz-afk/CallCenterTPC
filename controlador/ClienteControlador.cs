using System;
using System.Web.Mvc; 
using CallCenterTPC.Dominio;
using CallCenterTPC.Datos;

namespace CallCenterTPC.Controlador
{
    public class ClientesController
    {
        // Instanciamos el repositorio que armaste en la capa de Datos
        private ClienteRepository _repo = new ClienteRepository();

        // ==========================================
        // PANTALLA PRINCIPAL: Listado de Clientes
        // ==========================================
        public ActionResult Index()
        {
            // Buscamos los clientes en la BD y se los pasamos a la vista
            var listaDeClientes = _repo.ObtenerTodos();
            return View(listaDeClientes);
        }

        // ==========================================
        // PANTALLA DE ALTA: Mostrar el formulario en blanco
        // ==========================================
        [HttpGet]
        public ActionResult Crear()
        {
            return View();
        }

        // ==========================================
        // ACCIÓN DE ALTA: Recibir los datos del formulario y guardar
        // ==========================================
        [HttpPost]
        public ActionResult Crear(Cliente nuevoCliente)
        {
            try
            {
                // Le pasamos el objeto lleno al repositorio para que haga el INSERT
                _repo.Crear(nuevoCliente);

                // Si todo sale bien, lo mandamos de vuelta a la grilla
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Si hay un error (ej. base de datos caída), volvemos a mostrar el formulario
                ViewBag.Error = "Ocurrió un error al guardar: " + ex.Message;
                return View(nuevoCliente);
            }
        }
    }
}
