using System.Net;
using System.Net.Mail;

namespace DistribucionRutas.Clases
{
    public class ClsEnvioEmail
    {
        public bool enviarCorreo(string[] correos, string asunto, string html)
        {
            string username = "";
            string password = "";

            // Configuración del cliente SMTP
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(username, password);

            // Creación del mensaje de correo electrónico
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(username);
            foreach (string correo in correos)
            {
                mailMessage.To.Add(correo);
            }
            mailMessage.Subject = asunto;
            mailMessage.Body = html;
            mailMessage.IsBodyHtml = true;

            // Envío del mensaje
            client.Send(mailMessage);
            return true;
        }
    }
}