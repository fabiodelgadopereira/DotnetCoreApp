using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using experimento_swagger.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace cadastro.Controllers {
    [Route ("api/[controller]")]
    [Authorize ()]
    [ApiController]
    public class ContatoController : Controller {

        private readonly IConfiguration _smtpConfig;

        public ContatoController (IConfiguration configuration) {

            _smtpConfig = configuration;
        }

        [HttpPost]
        public async Task Post ([FromBody] Contato value) {

            try {
                string toEmail = value.Email;

                MailMessage mail = new MailMessage () {
                    From = new MailAddress (_smtpConfig["EmailSettings:UsernameEmail"])
                };

                mail.To.Add (new MailAddress (toEmail));
                mail.Subject = "[Contato] - " + value.Nome;
                mail.Body = value.Mensagem;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                //para anexos
                //mail.Attachments.Add(new Attachment(arquivo));
                //

                using (SmtpClient smtp = new SmtpClient (_smtpConfig["EmailSettings:PrimaryDomain"],  Int32.Parse(_smtpConfig["EmailSettings:PrimaryPort"]))) {
                    smtp.Credentials = new NetworkCredential (_smtpConfig["EmailSettings:UsernameEmail"],_smtpConfig["EmailSettings:UsernamePassword"]);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync (mail);
                }
            } catch (Exception ex) {
                throw ex;
            }
        }

    }
}