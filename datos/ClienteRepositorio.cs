using System;
using System.Collections.Generic;
using CallCenterTPC.Dominio;

namespace CallCenterTPC.Datos
{
    public class ClienteRepositorio
    {
        public void Crear(Cliente cliente)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"INSERT INTO clientes (nombre, apellido, documento, email, telefono, activo, fecha_creacion) 
                                VALUES (@nombre, @apellido, @documento, @email, @telefono, 1, GETDATE())";

                datos.setearConsulta(consulta);
                datos.setearParametro("@nombre", cliente.nombre);
                datos.setearParametro("@apellido", cliente.apellido);
                datos.setearParametro("@documento", cliente.documento);
                datos.setearParametro("@email", cliente.email);
                datos.setearParametro("@telefono", cliente.telefono);
                // activo lo puse en 1 y la fecha_creacion en GETDATE() directo en el SQL

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex; // O manejar el error según la arquitectura de tu app
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // ==========================================
        // MODIFICACIÓN (Update)
        // ==========================================
        public void Actualizar(Cliente cliente)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"UPDATE clientes 
                                SET nombre = @nombre, apellido = @apellido, documento = @documento, 
                                    email = @email, telefono = @telefono, activo = @activo 
                                WHERE id = @id";

                datos.setearConsulta(consulta);
                datos.setearParametro("@id", cliente.id);
                datos.setearParametro("@nombre", cliente.nombre);
                datos.setearParametro("@apellido", cliente.apellido);
                datos.setearParametro("@documento", cliente.documento);
                datos.setearParametro("@email", cliente.email);
                datos.setearParametro("@telefono", cliente.telefono);
                datos.setearParametro("@activo", cliente.activo);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // ==========================================
        // BAJA LÓGICA (Soft Delete)
        // ==========================================
        public void DarDeBaja(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE clientes SET activo = 0 WHERE id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // ==========================================
        // LECTURA (Read All)
        // ==========================================
        public List<Cliente> ObtenerTodos()
        {
            List<Cliente> lista = new List<Cliente>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT id, nombre, apellido, documento, email, telefono, activo, fecha_creacion FROM clientes WHERE activo = 1");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Cliente aux = new Cliente();

                    // Mapeo manual de la base de datos al objeto C#
                    aux.id = (int)datos.Lector["id"];
                    aux.nombre = (string)datos.Lector["nombre"];
                    aux.apellido = (string)datos.Lector["apellido"];
                    aux.documento = (int)datos.Lector["documento"];

                    // Validación por si algún string viene nulo (muy buena práctica en BD)
                    if (!(datos.Lector["email"] is DBNull))
                        aux.email = (string)datos.Lector["email"];

                    aux.telefono = (int)datos.Lector["telefono"];
                    aux.activo = (bool)datos.Lector["activo"];
                    aux.fechaCreacion = (DateTime)datos.Lector["fecha_creacion"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

    }
}
