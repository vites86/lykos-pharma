using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Olga.BLL.Interfaces
{
    public interface IBaseEmailService
    {
        string Login { get; set; }
        string Pass { get; set; }
        string From { get; set; }
        string DirectorMail { get; set; }
        string DeveloperMail { get; set; }
        Task SendEmailNotification(string body, string subject, IEnumerable<string> emailEnumerable = null);
        Task Send(IdentityMessage message, IEnumerable<string> emailEnumerable = null);
    }
}
