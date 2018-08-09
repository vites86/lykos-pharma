using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olga.DAL.Entities.Account
{
    public class ClientProfile
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }

        public string Name { get; set; }
        public string Rank { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<Country> Countries { get; set; }

        public ClientProfile()
        {
            Countries = new List<Country>();
        }
    }

    public enum Roles
    {
        Admin,
        Manager,
        User,
        Quality
    }
}
