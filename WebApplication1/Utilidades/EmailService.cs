using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace CallCenterTPC.Utilidades
{
    public class EmailService
    {
        private MailMessage email;
        private SmtpClient server;

        public EmailService()
        {
            server = new SmtpClient();
            server.Credentials = new NetworkCredential("test@gmail.com", "asdads");
            server.EnableSsl = true;
            server.Port = 587;
            server.Host = "smtp.gmail.com"; 
        }

        public void ArmarCorreo(string destino, string asunto, string cuerpo)
        {
            email = new MailMessage();
            email.From = new MailAddress("no-responder@callcenter.com");
            email.To.Add(destino);
            email.Subject = asunto;
            email.Body = cuerpo;
            email.IsBodyHtml = true; 
        }

        public void EnviarEmail()
        {
            try
            {
                server.Send(email);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}