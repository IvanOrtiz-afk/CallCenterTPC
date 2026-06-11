using System.Collections.Generic;
using CallCenterTPC.Datos;
using CallCenterTPC.Dominio;

namespace CallCenterTPC.Negocio
{
    public class ClienteNegocio
    {
        public List<Cliente> listar()
        {
            List<Cliente> lista = new List<Cliente>();

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT Id, Nombre, Apellido, DNI, Email, Telefono, Activo FROM Clientes");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Cliente aux = new Cliente();

                    aux.id = (int)datos.Lector["id"];
                    aux.nombre = (string)datos.Lector["nombre"];
                    aux.apellido = (string)datos.Lector["apellido"];
                    aux.documento = (int)datos.Lector["documento"];
                    aux.email = (string)datos.Lector["email"];
                    aux.telefono = (int)datos.Lector["telefono"];
                    aux.activo = (bool)datos.Lector["activo"];

                    lista.Add(aux);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}