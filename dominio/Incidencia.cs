using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace CallCenterTPC.Dominio
{
    public class Incidencia
    {
        public int id { get; set; }
        public int clienteId { get; set; }
     
        public Cliente cliente { get; set; }
        public TipoIncidencia tipoIncidencia { get; set; }
        public Prioridad prioridad { get; set; }
        public EstadoIncidencia estado { get; set; }
        public string asunto { get; set; }

        public Usuario usuarioCreador { get; set; }
        public Usuario usuarioAsignado { get; set; }

        public int tipoIncidenciaId { get; set; }
        public int prioridadId { get; set; }
        public int estadoId { get; set; }
        public int usuarioCreadorId { get; set; }
        public int? usuarioAsignadoId { get; set; }
        public string descripcion { get; set; }
        public string comentarioCierre { get; set; }
        public string comentarioResolucion { get; set; }
        public DateTime fechaAlta { get; set; }
        public DateTime? fechaResolucion { get; set; }
        public DateTime? fechaCierre { get; set; }
    }
}