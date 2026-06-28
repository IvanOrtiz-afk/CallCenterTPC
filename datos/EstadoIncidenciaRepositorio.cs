using CallCenterTPC.Datos;
using CallCenterTPC.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenterTPC.Datos
{
    public class EstadoIncidenciaRepositorio
    {

        public List<EstadoIncidencia> Listar()
        {
            List<EstadoIncidencia> lista = new List<EstadoIncidencia>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                
                datos.setearConsulta("SELECT id, nombre FROM [estados]");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    EstadoIncidencia aux = new EstadoIncidencia();

                    aux.id = (int)datos.Lector["id"];
                    aux.nombre = (string)datos.Lector["nombre"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar listar (GetAll) los estados: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

    }
}
