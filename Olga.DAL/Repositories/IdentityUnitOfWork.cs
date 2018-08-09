using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Olga.DAL.EF;
using Olga.DAL.Entities;
using Olga.DAL.Entities.Account;
using Olga.DAL.Identity;
using Olga.DAL.Interfaces;

namespace Olga.DAL.Repositories
{
    public class IdentityUnitOfWork : IUnitOfWork
    {
        //private AccountContext db;
        private ProductContext db;

        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;
        private IClientManager clientManager;

        public IdentityUnitOfWork(string connectionString)
        {
            // db = new AccountContext(connectionString);
            db = new ProductContext(connectionString);
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
            clientManager = new ClientManager(db);
        }

        public virtual DbContext GetContext()
        {
            return db;
        }

        public IQueryable<ClientProfile> GetAll()
        {
            return db.ClientProfiles;
        }

        public ApplicationUserManager UserManager
        {
            get { return userManager; }
        }

        public IClientManager ClientManager
        {
            get { return clientManager; }
        }

        public ApplicationRoleManager RoleManager
        {
            get { return roleManager; }
        }

        public Country GetCountry(int id)
        {
            if (id == 0) return null;
            return db.Countries.Find(id);
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    userManager.Dispose();
                    roleManager.Dispose();
                    clientManager.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}
