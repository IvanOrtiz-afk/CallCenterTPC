using System;
using System.Collections.Generic;
using CallCenterTPC.Dominio;

namespace CallCenterTPC.Datos
{
    public class ClienteRepositorio
    {

        public List<Cliente> Listar()
        {
            List<Cliente> lista = new List<Cliente>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Traemos todos los campos mapeados en la tabla clientes
                datos.setearConsulta("SELECT id, nombre, apellido, documento, email, telefono, activo, fecha_creacion FROM [clientes]");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Cliente aux = new Cliente();

                    aux.id = (int)datos.Lector["id"];
                    aux.nombre = (string)datos.Lector["nombre"];
                    aux.apellido = (string)datos.Lector["apellido"];
                    aux.documento = (int)datos.Lector["documento"];

                    
                    if (!(datos.Lector["email"] is DBNull))
                        aux.email = (string)datos.Lector["email"];

                   
                    if (!(datos.Lector["telefono"] is DBNull))
                        aux.telefono = Convert.ToInt32(datos.Lector["telefono"]);

                    aux.activo = (bool)datos.Lector["activo"];

                    
                    if (!(datos.Lector["fecha_creacion"] is DBNull))
                        aux.fechaCreacion = (DateTime)datos.Lector["fecha_creacion"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar listar los clientes: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Agregar(Cliente cliente)
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

                    aux.id = (int)datos.Lector["id"];
                    aux.nombre = (string)datos.Lector["nombre"];
                    aux.apellido = (string)datos.Lector["apellido"];
                    aux.documento = (int)datos.Lector["documento"];

                    if (!(datos.Lector["email"] is DBNull))
                        aux.email = (string)datos.Lector["email"];

                    // ACÁ ESTÁ EL ARREGLO: Convertimos el varchar de SQL al int de C#
                    if (!(datos.Lector["telefono"] is DBNull))
                        aux.telefono = Convert.ToInt32(datos.Lector["telefono"]);

                    aux.activo = (bool)datos.Lector["activo"];

                    if (!(datos.Lector["fecha_creacion"] is DBNull))
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

        public bool ExisteEmail(string email)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT id FROM clientes WHERE email = @email");
                datos.setearParametro("@email", email);

                datos.ejecutarLectura();

                return datos.Lector.Read();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar email: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public bool ExisteDocumento(int documento)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT id FROM clientes WHERE documento = @documento");
                datos.setearParametro("@documento", documento);

                datos.ejecutarLectura();

                return datos.Lector.Read();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar documento: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

    }
}
