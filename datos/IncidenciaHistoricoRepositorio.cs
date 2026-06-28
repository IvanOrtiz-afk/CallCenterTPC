using CallCenterTPC.Datos;
using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenterTPC.Datos
{
    public class IncidenciaHistoricoRepositorio
    {

      
        public List<IncidenciaHistorico> Listar()
        {
            List<IncidenciaHistorico> lista = new List<IncidenciaHistorico>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
              
                datos.setearConsulta("SELECT id, id_incidencia, estado_incidencia_id, tipo_incidencia_id, " +
                                     "cliente_id, usuario_id, prioridad_incidencia_id, fecha " +
                                     "FROM [incidencias_historico]");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    IncidenciaHistorico aux = new IncidenciaHistorico();

                    aux.id = (int)datos.Lector["id"];
                    aux.idIncidencia = (int)datos.Lector["id_incidencia"];
                    aux.estadoIncidenciaId = (int)datos.Lector["estado_incidencia_id"];
                    aux.tipoIncidenciaId = (int)datos.Lector["tipo_incidencia_id"];
                    aux.clienteId = (int)datos.Lector["cliente_id"];
                    aux.usuarioId = (int)datos.Lector["usuario_id"];
                    aux.prioridadIncidenciaId = (int)datos.Lector["prioridad_incidencia_id"];
                    aux.fecha = (DateTime)datos.Lector["fecha"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar listar (GetAll) el historial de incidencias: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

    }
}
