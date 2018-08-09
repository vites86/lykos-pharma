using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Olga.DAL.Entities;
using Olga.DAL.Entities.Account;

namespace Olga.DAL.EF
{
    public class ProductContext : IdentityDbContext<ApplicationUser>
    {
        public ProductContext() : base("name=DefaultConectionString") { }
        public ProductContext(string connectionString) : base(connectionString) { }

        public DbSet<ClientProfile> ClientProfiles { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<ApprDocsType> ApprDocsTypes { get; set; }
        public DbSet<Artwork> Artworks { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<MarketingAuthorizHolder> MarketingAuthorizHolders { get; set; }
        public DbSet<MarketingAuthorizNumber> MarketingAuthorizNumbers { get; set; }
        public DbSet<PackSize> PackSizes { get; set; }
        public DbSet<PharmaceuticalForm> PharmaceuticalForms { get; set; }
        public DbSet<ProductCode> ProductCodes { get; set; }
        public DbSet<ProductName> ProductNames { get; set; }
        public DbSet<Strength> Strengths { get; set; }
        public DbSet<ProductDocument> ProductDocuments { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<Remark> Remarks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }

       

    }


}
