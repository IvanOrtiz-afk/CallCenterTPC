using CallCenterTPC.Datos;
using CallCenterTPC.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenterTPC.Datos
{
    public class TipoIncidenciaRepositorio
    {

        public List<TipoIncidencia> Listar()
        {
            List<TipoIncidencia> lista = new List<TipoIncidencia>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT id, nombre FROM [tipos_incidencias]");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    TipoIncidencia aux = new TipoIncidencia();

                    aux.id = (int)datos.Lector["id"];
                    aux.nombre = (string)datos.Lector["nombre"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar listar los tipos de incidencia: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public void Agregar(TipoIncidencia nuevoTipo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // El ID es IDENTITY, por lo que solo insertamos el nombre
                datos.setearConsulta("INSERT INTO [tipos_incidencias] (nombre) VALUES (@nombre)");
                datos.setearParametro("@nombre", nuevoTipo.nombre);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar agregar el tipo de incidencia: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // --- MODIFICACIÓN ---
        public void Modificar(TipoIncidencia tipo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Restricción aplicada: solo se permite cambiar el nombre
                datos.setearConsulta("UPDATE [tipos_incidencias] SET nombre = @nombre WHERE id = @id");

                datos.setearParametro("@id", tipo.id);
                datos.setearParametro("@nombre", tipo.nombre);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar modificar el tipo de incidencia: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // --- BAJA (Física con validación de relaciones) ---
        public void Eliminar(int idTipoIncidencia)
        {
            // 1. Instancia para verificar si existen incidencias asociadas a este tipo
            AccesoDatos datosValidacion = new AccesoDatos();

            try
            {
                // Buscamos si existe al menos una incidencia con este tipo_incidencia_id
                datosValidacion.setearConsulta("SELECT COUNT(*) AS Cantidad FROM [incidencias] WHERE tipo_incidencia_id = @id");
                datosValidacion.setearParametro("@id", idTipoIncidencia);
                datosValidacion.ejecutarLectura();

                if (datosValidacion.Lector.Read())
                {
                    int cantidadAsociadas = (int)datosValidacion.Lector["Cantidad"];

                    // Validamos la regla de negocio
                    if (cantidadAsociadas > 0)
                    {
                        throw new Exception($"No se puede eliminar el tipo de incidencia porque está asociado a {cantidadAsociadas} incidencia(s).");
                    }
                }
            }
            catch (Exception ex)
            {
                // Relanzamos la excepción para cortar la ejecución
                throw ex;
            }
            finally
            {
                // Cerramos la conexión de lectura
                datosValidacion.cerrarConexion();
            }

            // 2. Si no hay incidencias asociadas, procedemos con el borrado físico
            AccesoDatos datosBorrado = new AccesoDatos();

            try
            {
                datosBorrado.setearConsulta("DELETE FROM [tipos_incidencias] WHERE id = @id");
                datosBorrado.setearParametro("@id", idTipoIncidencia);

                datosBorrado.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar eliminar el tipo de incidencia: " + ex.Message);
            }
            finally
            {
                datosBorrado.cerrarConexion();
            }
        }

    }
}
