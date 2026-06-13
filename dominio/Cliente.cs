using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallCenterTPC.Dominio
{
    public class Cliente
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int documento { get; set; }
        public string email { get; set; }
        public int telefono { get; set; }
        public bool activo { get; set; }
        public DateTime fechaCreacion { get; set; }
    }
}