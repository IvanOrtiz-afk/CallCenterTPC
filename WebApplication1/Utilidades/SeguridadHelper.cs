using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CallCenterTPC.Dominio;

namespace CallCenterTPC.Utilidades
{
    public static class SeguridadHelper
    {
        public static Usuario UsuarioActual()
        {
            return (Usuario)HttpContext.Current.Session["Usuario"];
        }

        public static bool HaySesion()
        {
            return UsuarioActual() != null;
        }

        public static bool TienePermiso(string permiso)
        {
            Usuario usuario = UsuarioActual();

            if (usuario == null)
                return false;

            switch (usuario.rolId)
            {
                // Administrador
                case 1:
                    return true;

                // Telefonista (Rol 2)
                case 2:
                    return permiso == Permisos.Clientes
                        || permiso == Permisos.Incidencias;

                // Supervisor (Rol 3)
                case 3:
                    return permiso == Permisos.Clientes
                        || permiso == Permisos.Incidencias
                        || permiso == Permisos.ReasignarIncidencias;

                default:
                    return false;
            }
        }

        public static bool EsAdmin()
        {
            Usuario usuario = UsuarioActual();

            return usuario != null &&
                   usuario.rolId == 1;
        }

        public static bool EsAgente()
        {
            Usuario usuario = UsuarioActual();

            return usuario != null &&
                   usuario.rolId == 2;
        }

        public static bool EsCoordinador()
        {
            Usuario usuario = UsuarioActual();

            return usuario != null &&
                   usuario.rolId == 3;
        }
        public static bool EsAdminOCoordinador()
        {
            Usuario usuario = UsuarioActual();

            return usuario != null &&
                   (usuario.rolId == 1 || usuario.rolId == 3);
        }
    }
}