using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class IncidenciaHistorico
    {
        public int id { get; set; }
        public int idIncidencia { get; set; }
        public int estadoIncidenciaId { get; set; }
        public int tipoIncidenciaId { get; set; }
        public int clienteId { get; set; }
        public int usuarioId { get; set; }
        public int prioridadIncidenciaId { get; set; }
        public DateTime fecha { get; set; }
    }
}
