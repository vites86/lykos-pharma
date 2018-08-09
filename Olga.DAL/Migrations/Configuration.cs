using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Olga.DAL.Entities.Account;

namespace Olga.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Olga.DAL.EF.ProductContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Olga.DAL.EF.ProductContext context)
        {
           // AddRoles(context);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }

        public static void AddRoles(DbContext db)
        {
            var roleStore = new RoleStore<IdentityRole>(db);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            CreateRole(roleManager, Roles.Admin);
            CreateRole(roleManager, Roles.Manager);
            CreateRole(roleManager, Roles.User);
            CreateRole(roleManager, Roles.Quality);
            db.SaveChanges();
        }

        private static void CreateRole(RoleManager<IdentityRole> roleManager, Roles roleCreate)
        {
            var role = roleManager.FindByName(roleCreate.ToString());
            if (role == null)
            {
                role = new IdentityRole(roleCreate.ToString());
                roleManager.Create(role);
            }
        }
    }
}
