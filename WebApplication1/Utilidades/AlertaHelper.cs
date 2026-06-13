using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace CallCenterTPC.Utilidades
{
    public static class AlertaHelper
    {
        // Muestra cualquier alerta en la página actual
        public static void MostrarAlerta(Panel pnl, Label lbl, string mensaje, bool esError)
        {
            pnl.Visible = true;
            pnl.CssClass = esError ? "alert alert-danger alert-dismissible fade show"
                                   : "alert alert-success alert-dismissible fade show";
            lbl.Text = mensaje;
        }

        // Guarda el mensaje para después de una redirección
        public static void GuardarMensajeExito(string mensaje)
        {
            HttpContext.Current.Session["MensajeExito"] = mensaje;
        }

        // Se usa en el Page_Load de la página destino
        public static void CargarMensajeRedirigido(Panel pnl, Label lbl)
        {
            if (HttpContext.Current.Session["MensajeExito"] != null)
            {
                MostrarAlerta(pnl, lbl, HttpContext.Current.Session["MensajeExito"].ToString(), false);
                HttpContext.Current.Session.Remove("MensajeExito");
            }
        }
    }
}