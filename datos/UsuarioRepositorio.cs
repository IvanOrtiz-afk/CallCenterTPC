using CallCenterTPC.Datos;
using CallCenterTPC.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenterTPC.Datos

{
    public class UsuarioRepositorio
    {
        public void Agregar(Usuario nuevoUsuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO [usuarios] (nombre, apellido, email, password, rol_id, activo, fecha_creacion) " +
                                     "VALUES (@nombre, @apellido, @email, @password, @rolId, 1, GETDATE())");

                datos.setearParametro("@nombre", nuevoUsuario.nombre);
                datos.setearParametro("@apellido", nuevoUsuario.apellido);
                datos.setearParametro("@email", nuevoUsuario.email);
                datos.setearParametro("@password", nuevoUsuario.password);
                datos.setearParametro("@rolId", nuevoUsuario.rolId);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar agregar el usuario: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // --- MODIFICACIÓN ---
        public void Modificar(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE [usuarios] SET nombre = @nombre, apellido = @apellido, " +
                                     "email = @email, password = @password, rol_id = @rolId, activo = @activo " +
                                     "WHERE id = @id");

                datos.setearParametro("@id", usuario.id);
                datos.setearParametro("@nombre", usuario.nombre);
                datos.setearParametro("@apellido", usuario.apellido);
                datos.setearParametro("@email", usuario.email);
                datos.setearParametro("@password", usuario.password);
                datos.setearParametro("@rolId", usuario.rolId);
                datos.setearParametro("@activo", usuario.activo);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar modificar el usuario: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // --- BAJA (Lógica) ---
        public void Eliminar(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE [usuarios] SET activo = 0 WHERE id = @id");
                datos.setearParametro("@id", idUsuario);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar dar de baja el usuario: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // --- LECTURA (Ejemplo para listar usuarios) ---
        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT id, nombre, apellido, email, password, rol_id, activo, fecha_creacion FROM [usuarios]");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario aux = new Usuario();
                    aux.id = (int)datos.Lector["id"];
                    aux.nombre = (string)datos.Lector["nombre"];
                    aux.apellido = (string)datos.Lector["apellido"];
                    aux.email = (string)datos.Lector["email"];
                    aux.password = (string)datos.Lector["password"];
                    aux.rolId = (int)datos.Lector["rol_id"];
                    aux.activo = (bool)datos.Lector["activo"];
                    aux.fechaCreacion = (DateTime)datos.Lector["fecha_creacion"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar listar los usuarios: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Usuario> ListarAgentes()
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            SELECT id,
                   nombre,
                   apellido,
                   email,
                   rol_id,
                   activo
            FROM usuarios
            WHERE rol_id = 3
              AND activo = 1
            ORDER BY apellido, nombre");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario aux = new Usuario();

                    aux.id = (int)datos.Lector["id"];
                    aux.nombre = datos.Lector["nombre"].ToString();
                    aux.apellido = datos.Lector["apellido"].ToString();
                    aux.email = datos.Lector["email"].ToString();
                    aux.rolId = (int)datos.Lector["rol_id"];
                    aux.activo = (bool)datos.Lector["activo"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar agentes: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public Usuario ObtenerPorEmail(string email)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT id, nombre, apellido, email, password, rol_id, activo, fecha_creacion FROM usuarios WHERE email = @email");
                datos.setearParametro("@email", email);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Usuario aux = new Usuario();

                    aux.id = (int)datos.Lector["id"];
                    aux.nombre = (string)datos.Lector["nombre"];
                    aux.apellido = (string)datos.Lector["apellido"];
                    aux.email = (string)datos.Lector["email"];
                    aux.password = (string)datos.Lector["password"];
                    aux.rolId = (int)datos.Lector["rol_id"];
                    aux.activo = (bool)datos.Lector["activo"];
                    aux.fechaCreacion = (DateTime)datos.Lector["fecha_creacion"];

                    return aux;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener usuario: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public Usuario ObtenerPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "SELECT id, nombre, apellido, email, password, rol_id, activo, fecha_creacion " +
                    "FROM usuarios WHERE id = @id");

                datos.setearParametro("@id", id);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Usuario aux = new Usuario();

                    aux.id = (int)datos.Lector["id"];
                    aux.nombre = (string)datos.Lector["nombre"];
                    aux.apellido = (string)datos.Lector["apellido"];
                    aux.email = (string)datos.Lector["email"];
                    aux.password = (string)datos.Lector["password"];
                    aux.rolId = (int)datos.Lector["rol_id"];
                    aux.activo = (bool)datos.Lector["activo"];
                    aux.fechaCreacion = (DateTime)datos.Lector["fecha_creacion"];

                    return aux;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener usuario: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public bool ExisteEmail(string email) //no deja duplicar email
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "SELECT id FROM usuarios WHERE email = @email");

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
    }
}
