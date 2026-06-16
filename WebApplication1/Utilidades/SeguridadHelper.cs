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
                   usuario.rolId == 3;
        }

        public static bool EsCoordinador()
        {
            Usuario usuario = UsuarioActual();

            return usuario != null &&
                   usuario.rolId == 4;
        }
        public static bool EsAdminOCoordinador()
        {
            Usuario usuario = UsuarioActual();

            return usuario != null &&
                   (usuario.rolId == 1 || usuario.rolId == 4);
        }
    }
}