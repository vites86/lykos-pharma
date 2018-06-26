using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Olga.DAL.Entities.Account;

namespace Olga.DAL.EF
{
    public class AccountContext : IdentityDbContext<ApplicationUser>
    {
        public AccountContext(string conectionString) : base(conectionString) { }
        public DbSet<ClientProfile> ClientProfiles { get; set; }
    }
}
