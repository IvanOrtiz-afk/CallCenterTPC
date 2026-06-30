using CallCenterTPC.Datos;
using CallCenterTPC.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenterTPC.Datos
{
    public class PrioridadRepositorio
    {

        public List<Prioridad> Listar()
        {
            List<Prioridad> lista = new List<Prioridad>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT id, nombre FROM [prioridades]");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Prioridad aux = new Prioridad();

                    aux.id = (int)datos.Lector["id"];
                    aux.nombre = (string)datos.Lector["nombre"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar listar las prioridades: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Agregar(Prioridad nuevaPrioridad)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Solo insertamos el nombre, el ID es IDENTITY (autonumérico)
                datos.setearConsulta("INSERT INTO [prioridades] (nombre) VALUES (@nombre)");
                datos.setearParametro("@nombre", nuevaPrioridad.nombre);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar agregar la prioridad: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

      
        public void Modificar(Prioridad prioridad)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Se respeta la regla: SOLO se puede cambiar el nombre.
                datos.setearConsulta("UPDATE [prioridades] SET nombre = @nombre WHERE id = @id");

                datos.setearParametro("@id", prioridad.id);
                datos.setearParametro("@nombre", prioridad.nombre);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar modificar la prioridad: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

     
        public void Eliminar(int idPrioridad)
        {
          
            AccesoDatos datosValidacion = new AccesoDatos();

            try
            {
                
                datosValidacion.setearConsulta("SELECT COUNT(*) AS Cantidad FROM [incidencias] WHERE prioridad_id = @id");
                datosValidacion.setearParametro("@id", idPrioridad);
                datosValidacion.ejecutarLectura();

                if (datosValidacion.Lector.Read())
                {
                    int cantidadAsociadas = (int)datosValidacion.Lector["Cantidad"];

                  
                    if (cantidadAsociadas > 0)
                    {
                        throw new Exception($"No se puede eliminar la prioridad porque está asociada a {cantidadAsociadas} incidencia(s).");
                    }
                }
            }
            catch (Exception ex)
            {
               
                throw ex;
            }
            finally
            {
              
                datosValidacion.cerrarConexion();
            }

         
            AccesoDatos datosBorrado = new AccesoDatos();

            try
            {
                datosBorrado.setearConsulta("DELETE FROM [prioridades] WHERE id = @id");
                datosBorrado.setearParametro("@id", idPrioridad);

                datosBorrado.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar eliminar la prioridad: " + ex.Message);
            }
            finally
            {
                datosBorrado.cerrarConexion();
            }
        }
    }
}
