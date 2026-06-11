using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallCenterTPC.Dominio
{
    public class Cliente
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string documento { get; set; }

        public string Email { get; set; }

        public string Telefono { get; set; }

        public bool Activo { get; set; }
    }
}