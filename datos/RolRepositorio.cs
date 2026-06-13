using CallCenterTPC.Datos;
using CallCenterTPC.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenterTPC.Datos
{
    public class RolRepositorio
    {

        public List<Rol> Listar()
        {
            List<Rol> lista = new List<Rol>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Solo necesitamos el ID y el nombre del rol
                datos.setearConsulta("SELECT id, nombre FROM [roles]");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Rol aux = new Rol();

                    aux.id = (int)datos.Lector["id"];
                    aux.nombre = (string)datos.Lector["nombre"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar listar (GetAll) los roles: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

    }
}
