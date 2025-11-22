using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;

namespace SGC.LogicaDeNegocio
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly IConfiguration _config;

        public SmtpEmailSender(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                //Console.WriteLine($"[SMTP] Intentando enviar correo a {email}...");

                var smtpServer = _config["Smtp:Server"];
                var smtpPort = int.Parse(_config["Smtp:Port"]);
                var smtpUser = _config["Smtp:User"];
                var smtpPass = _config["Smtp:Pass"];
                var fromEmail = _config["Smtp:FromEmail"];

                using (var client = new SmtpClient(smtpServer, smtpPort))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(smtpUser, smtpPass);

                    var mail = new MailMessage
                    {
                        From = new MailAddress(fromEmail, "SGC - Sistema de Créditos"),
                        Subject = subject,
                        Body = htmlMessage,
                        IsBodyHtml = true
                    };

                    mail.To.Add(email);

                    await client.SendMailAsync(mail);
                }

                //Console.WriteLine($"[SMTP] ✅ Correo enviado correctamente a {email}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SMTP] ❌ Error al enviar correo: {ex.Message}");
                if (ex.InnerException != null)
                    Console.WriteLine($"[SMTP] Detalle: {ex.InnerException.Message}");
            }
        }
    }
}
