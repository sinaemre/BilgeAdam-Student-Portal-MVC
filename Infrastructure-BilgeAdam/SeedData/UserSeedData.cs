using Infrastructure_BilgeAdam.Context;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure_BilgeAdam.Context.IdentityContext;
using ApplicationCore_BilgeAdam.Entities.UserEntities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure_BilgeAdam.SeedData
{
    public class UserSeedData : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            var hasher = new PasswordHasher<AppUser>();

            var admin = new AppUser
            {
                Id = "e3569982-6bcc-49ed-b599-6b67ae72d134",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@test.com",
                NormalizedEmail = "ADMIN@TEST.COM",
                PasswordHash = hasher.HashPassword(null, "123"),
                LockoutEnabled = true
            };

            var student = new AppUser
            {
                Id = "cea12c00-6fae-4243-ae02-e9498d11a3e5",
                UserName = "student",
                NormalizedUserName = "STUDENT",
                Email = "student@test.com",
                NormalizedEmail = "STUDENT@TEST.COM",
                PasswordHash = hasher.HashPassword(null, "123"),
                LockoutEnabled = true
            };

            var student2 = new AppUser
            {
                Id = "c0333990-76bc-41c7-9b36-f4b99b6aa33d",
                UserName = "student2",
                NormalizedUserName = "STUDENT2",
                Email = "student2@test.com",
                NormalizedEmail = "STUDENT2@TEST.COM",
                PasswordHash = hasher.HashPassword(null, "123"),
                LockoutEnabled = true
            };

            var teacher = new AppUser
            {
                Id = "d7e6ce89-4fdb-4dab-b53f-0d55874de67b",
                UserName = "teacher",
                NormalizedUserName = "TEACHER",
                Email = "teacher@test.com",
                NormalizedEmail = "TEACHER@TEST.COM",
                PasswordHash = hasher.HashPassword(null, "123"),
                LockoutEnabled = true
            };

            var ikPersonel = new AppUser
            {
                Id = "b83692b7-aaa5-41c9-a97b-5123da9f6eb0",
                UserName = "ikPersonel",
                NormalizedUserName = "IKPERSONEL",
                Email = "ikpersonel@test.com",
                NormalizedEmail = "IKPERSONEL@TEST.COM",
                PasswordHash = hasher.HashPassword(null, "123"),
                LockoutEnabled = true
            };

            builder.HasData
            (
                admin, student, student2, teacher, ikPersonel
            );
        }

        

    }
}
