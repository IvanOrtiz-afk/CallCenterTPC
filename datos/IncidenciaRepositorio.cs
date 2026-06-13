using CallCenterTPC.Datos;
using CallCenterTPC.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenterTPC.Datos
{
    public class IncidenciaRepositorio
    {

        public List<Incidencia> Listar()
        {
            List<Incidencia> lista = new List<Incidencia>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Hacemos INNER JOIN para traer los nombres descriptivos
                datos.setearConsulta(@"SELECT i.id, i.cliente_id, c.nombre AS nombreCliente, c.apellido AS apellidoCliente, 
                                      i.tipo_incidencia_id, t.nombre AS nombreTipo, 
                                      i.prioridad_id, p.nombre AS nombrePrioridad, 
                                      i.estado_id, e.nombre AS nombreEstado, 
                                      i.descripcion, i.fecha_alta
                               FROM [incidencias] i
                               INNER JOIN [clientes] c ON i.cliente_id = c.id
                               INNER JOIN [tipos_incidencias] t ON i.tipo_incidencia_id = t.id
                               INNER JOIN [prioridades] p ON i.prioridad_id = p.id
                               INNER JOIN [estados] e ON i.estado_id = e.id");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Incidencia aux = new Incidencia();

                    aux.id = (int)datos.Lector["id"];
                    aux.descripcion = (string)datos.Lector["descripcion"];
                    aux.fechaAlta = (DateTime)datos.Lector["fecha_alta"];

                    // Mapeo de objetos relacionados
                    // Nota: Asumo que tu clase Incidencia tiene estas propiedades como objetos
                    aux.cliente = new Cliente();
                    aux.cliente.nombre = (string)datos.Lector["nombreCliente"] + " " + (string)datos.Lector["apellidoCliente"];

                    aux.tipoIncidencia = new TipoIncidencia();
                    aux.tipoIncidencia.nombre = (string)datos.Lector["nombreTipo"];

                    aux.prioridad = new Prioridad();
                    aux.prioridad.nombre = (string)datos.Lector["nombrePrioridad"];

                    aux.estado = new EstadoIncidencia();
                    aux.estado.nombre = (string)datos.Lector["nombreEstado"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar listar incidencias: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Agregar(Incidencia nuevaIncidencia)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Se asigna GETDATE() automáticamente para la fecha de alta.
                // Los comentarios y fechas de resolución/cierre no se insertan en el Alta porque nacen vacíos.
                datos.setearConsulta("INSERT INTO [incidencias] (cliente_id, tipo_incidencia_id, prioridad_id, " +
                                     "estado_id, usuario_creador_id, usuario_asignado_id, descripcion, fecha_alta) " +
                                     "VALUES (@clienteId, @tipoIncidenciaId, @prioridadId, @estadoId, " +
                                     "@usuarioCreadorId, @usuarioAsignadoId, @descripcion, GETDATE())");

                datos.setearParametro("@clienteId", nuevaIncidencia.clienteId);
                datos.setearParametro("@tipoIncidenciaId", nuevaIncidencia.tipoIncidenciaId);
                datos.setearParametro("@prioridadId", nuevaIncidencia.prioridadId);
                datos.setearParametro("@estadoId", nuevaIncidencia.estadoId);
                datos.setearParametro("@usuarioCreadorId", nuevaIncidencia.usuarioCreadorId);

                // IMPORTANTE: Si el usuario asignado es 0 (no asignado), tu Foreign Key podría fallar si no existe el ID 0 en la tabla usuarios. 
                // Asegúrate de enviar un ID válido o manejar esto como nulable (int?) en tu modelo.
                datos.setearParametro("@usuarioAsignadoId", nuevaIncidencia.usuarioAsignadoId);

                datos.setearParametro("@descripcion", nuevaIncidencia.descripcion ?? (object)DBNull.Value);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar agregar la incidencia: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // --- MODIFICACIÓN ---
        public void Modificar(Incidencia incidencia)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE [incidencias] SET cliente_id = @clienteId, tipo_incidencia_id = @tipoIncidenciaId, " +
                                     "prioridad_id = @prioridadId, estado_id = @estadoId, usuario_asignado_id = @usuarioAsignadoId, " +
                                     "descripcion = @descripcion, comentario_cierre = @comentarioCierre, " +
                                     "comentario_resolucion = @comentarioResolucion, fecha_resolucion = @fechaResolucion, " +
                                     "fecha_cierre = @fechaCierre WHERE id = @id");

                datos.setearParametro("@clienteId", incidencia.clienteId);
                datos.setearParametro("@tipoIncidenciaId", incidencia.tipoIncidenciaId);
                datos.setearParametro("@prioridadId", incidencia.prioridadId);
                datos.setearParametro("@estadoId", incidencia.estadoId);
                datos.setearParametro("@usuarioAsignadoId", incidencia.usuarioAsignadoId);

                // Manejo de posibles nulos (strings) usando el operador ??
                datos.setearParametro("@descripcion", incidencia.descripcion ?? (object)DBNull.Value);
                datos.setearParametro("@comentarioCierre", incidencia.comentarioCierre ?? (object)DBNull.Value);
                datos.setearParametro("@comentarioResolucion", incidencia.comentarioResolucion ?? (object)DBNull.Value);

                // Manejo de posibles nulos (DateTime?)
                datos.setearParametro("@fechaResolucion", (object)incidencia.fechaResolucion ?? DBNull.Value);
                datos.setearParametro("@fechaCierre", (object)incidencia.fechaCierre ?? DBNull.Value);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar modificar la incidencia: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // --- BAJA (Lógica por Estado) ---
        public void Eliminar(int idIncidencia, int idEstadoCancelado)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Como no hay columna 'activo', asumimos que "Eliminar" es pasar la incidencia a un estado cancelado/anulado.
                // Requerimos que nos pases qué ID de estado representa esa cancelación en tu tabla [estados].
                datos.setearConsulta("UPDATE [incidencias] SET estado_id = @estadoId, fecha_cierre = GETDATE() WHERE id = @id");

                datos.setearParametro("@id", idIncidencia);
                datos.setearParametro("@estadoId", idEstadoCancelado);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar cancelar (dar de baja) la incidencia: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

    }
}
