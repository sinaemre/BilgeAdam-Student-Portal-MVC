using ApplicationCore_BilgeAdam.Entities.UserEntities.Concrete;
using Infrastructure_BilgeAdam.SeedData;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_BilgeAdam.Context.IdentityContext
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options) 
        {
            //Update-Database komutunu kendi çalıştırır!
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserSeedData());
            builder.ApplyConfiguration(new RoleSeedData());
            builder.ApplyConfiguration(new IdentityUserRoleSeedData());
        }
    }
}
