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
                datos.setearConsulta(@"SELECT i.id, i.cliente_id, c.nombre AS nombreCliente, c.apellido AS apellidoCliente, 
                                      i.tipo_incidencia_id, t.nombre AS nombreTipo, 
                                      i.prioridad_id, p.nombre AS nombrePrioridad, 
                                      i.estado_id, e.nombre AS nombreEstado, i.asunto,
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
                    aux.asunto = datos.Lector["asunto"].ToString();
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

        public Incidencia ObtenerPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            SELECT
                i.id,
                i.cliente_id,
                c.nombre AS nombreCliente,
                c.apellido AS apellidoCliente,

                i.tipo_incidencia_id,
                t.nombre AS nombreTipo,

                i.prioridad_id,
                p.nombre AS nombrePrioridad,

                i.estado_id,
                e.nombre AS nombreEstado,

                i.usuario_creador_id,
                uc.nombre + ' ' + uc.apellido AS creador,

                i.usuario_asignado_id,
                ua.nombre + ' ' + ua.apellido AS asignado,
                
                i.asunto,
                i.descripcion,
                i.comentario_cierre,
                i.comentario_resolucion,
                i.fecha_alta,
                i.fecha_resolucion,
                i.fecha_cierre

            FROM incidencias i

            LEFT JOIN clientes c
                ON c.id = i.cliente_id

            LEFT JOIN tipos_incidencias t
                ON t.id = i.tipo_incidencia_id

            LEFT JOIN prioridades p
                ON p.id = i.prioridad_id

            LEFT JOIN estados e
                ON e.id = i.estado_id

            LEFT JOIN usuarios uc
                ON uc.id = i.usuario_creador_id

            LEFT JOIN usuarios ua
                ON ua.id = i.usuario_asignado_id

            WHERE i.id = @id");

                datos.setearParametro("@id", id);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Incidencia aux = new Incidencia();

                    aux.id = (int)datos.Lector["id"];

                    aux.clienteId = (int)datos.Lector["cliente_id"];
                    aux.tipoIncidenciaId = (int)datos.Lector["tipo_incidencia_id"];
                    aux.prioridadId = (int)datos.Lector["prioridad_id"];
                    aux.estadoId = (int)datos.Lector["estado_id"];
                    aux.usuarioCreadorId = (int)datos.Lector["usuario_creador_id"];
                    if (!(datos.Lector["usuario_asignado_id"] is DBNull))
                    {
                        aux.usuarioAsignadoId = (int)datos.Lector["usuario_asignado_id"];

                        aux.usuarioAsignado = new Usuario();
                        aux.usuarioAsignado.nombre = datos.Lector["asignado"].ToString();
                    }

                    aux.asunto = datos.Lector["asunto"].ToString();
                    aux.descripcion = datos.Lector["descripcion"].ToString();

                    if (!(datos.Lector["comentario_cierre"] is DBNull))
                        aux.comentarioCierre = datos.Lector["comentario_cierre"].ToString();

                    if (!(datos.Lector["comentario_resolucion"] is DBNull))
                        aux.comentarioResolucion = datos.Lector["comentario_resolucion"].ToString();

                    aux.fechaAlta = (DateTime)datos.Lector["fecha_alta"];

                    if (!(datos.Lector["fecha_resolucion"] is DBNull))
                        aux.fechaResolucion = (DateTime)datos.Lector["fecha_resolucion"];

                    if (!(datos.Lector["fecha_cierre"] is DBNull))
                        aux.fechaCierre = (DateTime)datos.Lector["fecha_cierre"];

                    aux.cliente = new Cliente();
                    aux.cliente.nombre = datos.Lector["nombreCliente"].ToString()
                                        + " "
                                        + datos.Lector["apellidoCliente"].ToString();

                    aux.tipoIncidencia = new TipoIncidencia();
                    aux.tipoIncidencia.nombre = datos.Lector["nombreTipo"].ToString();

                    aux.prioridad = new Prioridad();
                    aux.prioridad.nombre = datos.Lector["nombrePrioridad"].ToString();

                    aux.estado = new EstadoIncidencia();
                    aux.estado.nombre = datos.Lector["nombreEstado"].ToString();

                    aux.usuarioCreador = new Usuario();
                    aux.usuarioCreador.nombre = datos.Lector["creador"].ToString();

                    return aux;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la incidencia: " + ex.Message);
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
                datos.setearConsulta(@"
            INSERT INTO incidencias
            (
                cliente_id,
                tipo_incidencia_id,
                prioridad_id,
                estado_id,
                usuario_creador_id,
                usuario_asignado_id,
                asunto,
                descripcion,
                fecha_alta
            )
            VALUES
            (
                @clienteId,
                @tipoIncidenciaId,
                @prioridadId,
                @estadoId,
                @usuarioCreadorId,
                @usuarioAsignadoId,
                @asunto,
                @descripcion,
                GETDATE()
            )");

                datos.setearParametro("@clienteId", nuevaIncidencia.clienteId);
                datos.setearParametro("@tipoIncidenciaId", nuevaIncidencia.tipoIncidenciaId);
                datos.setearParametro("@prioridadId", nuevaIncidencia.prioridadId);
                datos.setearParametro("@estadoId", nuevaIncidencia.estadoId);
                datos.setearParametro("@usuarioCreadorId", nuevaIncidencia.usuarioCreadorId);

                if (nuevaIncidencia.usuarioAsignadoId.HasValue)
                    datos.setearParametro("@usuarioAsignadoId", nuevaIncidencia.usuarioAsignadoId.Value);
                else
                    datos.setearParametro("@usuarioAsignadoId", DBNull.Value);

                datos.setearParametro("@asunto", nuevaIncidencia.asunto);

                datos.setearParametro("@descripcion",
                    nuevaIncidencia.descripcion ?? (object)DBNull.Value);

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

        // modif
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
        public void ActualizarAsignacion(int idIncidencia, int idAgente)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            UPDATE incidencias
            SET usuario_asignado_id =
                @idAgente,

                estado_id =
                CASE
                    WHEN estado_id = 1 THEN 7
                    ELSE estado_id
                END

            WHERE id = @idIncidencia");

                datos.setearParametro("@idIncidencia", idIncidencia);
                datos.setearParametro("@idAgente", idAgente);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la asignación: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void Eliminar(int idIncidencia, int idEstadoCancelado)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                
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
