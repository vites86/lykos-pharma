using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Olga.Models
{
    public class Emailer
    {
        public string Login { get; set; }
        public string Pass { get; set; }
        public string From { get; set; }
        //From = "Regulatory Affairs Database notification";
        public string DirectorMail { get; set; }
        public string DeveloperMail { get; set; }
        public int Port { get; set; }
        public string SmtpServer { get; set; }
    }
}