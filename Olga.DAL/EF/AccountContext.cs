using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Olga.DAL.Entities.Account;

namespace Olga.DAL.EF
{
    //public class AccountContext : IdentityDbContext<ApplicationUser>
    //{
    //    public AccountContext(string conectionString) : base(conectionString) { }
    //    public AccountContext() : base("name=DefaultConectionString") { }

    //    public DbSet<ClientProfile> ClientProfiles { get; set; }

    //    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    //    {
    //        base.OnModelCreating(modelBuilder);

    //        //modelBuilder.Entity<IdentityUserLogin>()
    //        //    .HasMany(m => m.UserId)
    //        //    .WithRequired(m => m.PaymentSystemAccount)
    //        //    .HasForeignKey(m => m.PaymentSystemAccountId)
    //        //    .WillCascadeOnDelete(true);





    //        //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

    //        //modelBuilder.Entity<Product>()
    //        //    .HasMany(c => c.ApprDocsTypes)
    //        //    .WithMany(c => c.Products).Map(m =>
    //        //    {
    //        //        m.ToTable("Products_Refs_ApprDocsTypes");
    //        //        m.MapLeftKey("ProductId");
    //        //        m.MapRightKey("ApprDocsTypeId");
    //        //    });
    //        //base.OnModelCreating(modelBuilder);
    //    }
    //}
}
