using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Olga.BLL.DTO;

namespace Olga.BLL.Interfaces
{
    public interface IBaseEmailService
    {
        string Login { get; set; }
        string Pass { get; set; }
        string From { get; set; }
        string DirectorMail { get; set; }
        string DeveloperMail { get; set; }
        Task SendEmailNotification(string body, string subject, EmailerDTO emailerDTO, IEnumerable<string> emailEnumerable = null, bool send = true);
        Task Send(IdentityMessage message, IEnumerable<string> emailEnumerable = null);
    }
}
