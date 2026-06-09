using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace CallCenterTPC.Dominio
{
    public class Incidencia
    {
        public int Id { get; set; }

        public string NumeroReclamo { get; set; }

        public Cliente Cliente { get; set; }

        public TipoIncidencia TipoIncidencia { get; set; }

        public Prioridad Prioridad { get; set; }

        public EstadoIncidencia Estado { get; set; }

        public Usuario UsuarioCreador { get; set; }

        public Usuario UsuarioAsignado { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaAlta { get; set; }

        public DateTime? FechaResolucion { get; set; }

        public string ComentarioResolucion { get; set; }

        public DateTime? FechaCierre { get; set; }

        public string ComentarioCierre { get; set; }
    }
}