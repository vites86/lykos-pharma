using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Olga.BLL.Interfaces;
using log4net;
using Olga.BLL.DTO;


namespace Olga.BLL.Services
{
    public class BaseEmailerService : IBaseEmailService
    {
        public string Login { get; set; }
        public string Pass { get; set; }
        public string From { get; set; }
        public string DirectorMail { get; set; }
        public string DeveloperMail { get; set; }
        public string SmtpServ { get; set; }
        public int SmtpPort { get; set; }

        public BaseEmailerService()
        {
        }

        public async Task SendEmailNotification(string body, string subject, EmailerDTO emailerDto, IEnumerable<string> emailEnumerable = null, bool send = false)
        {
            if(!send) return;
            var message = new IdentityMessage
            {
                Subject = subject,
                Body = body
            };
            Login = emailerDto.Login;
            Pass = emailerDto.Pass;
            From = emailerDto.From;
            DirectorMail = emailerDto.DirectorMail;
            DeveloperMail = emailerDto.DeveloperMail;
            SmtpPort = emailerDto.Port;
            SmtpServ = emailerDto.SmtpServer;

           await Send(message, emailEnumerable);
        }

        public async Task Send(IdentityMessage message, IEnumerable<string> emailEnumerable = null)
        {
            try
            {
                using (SmtpClient client = new SmtpClient(SmtpServ,SmtpPort)
                {
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(Login, Pass),
                    EnableSsl = true
                })
                {
                    var mail = new MailMessage
                    {
                        From = new MailAddress(From, "Regulatory Affairs Database notification"),
                        Subject = message.Subject.Replace('\r', ' ').Replace('\n', ' '),
                        Body = message.Body,
                        IsBodyHtml = true
                    };

                    if (emailEnumerable != null)
                    {
                        foreach (var email in emailEnumerable)
                        {
                            mail.To.Add(email);
                        }
                        //mail.Bcc.Add("activeseach@gmail.com");
                    }

                    mail.To.Add(new MailAddress(DirectorMail));
                    mail.To.Add(new MailAddress(DeveloperMail));
                    await client.SendMailAsync(mail);
                    LoggerService.Log.Info($"SendEmailAboutAddUpdateProduct(): {message.Body}");
                }
            }
            catch (Exception ex)
            {
                LoggerService.Log.Error($"SendEmailAboutAddUpdateProduct(): {ex} ");
            }
        }
        
    }
}

