using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_BilgeAdam.SeedData
{
    public class RoleSeedData : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            var admin = new IdentityRole
            {
                Id = "2941c96f-580d-4fa1-a18f-80dc158b28cb",
                Name = "admin",
                NormalizedName = "ADMIN"
            };
            var student = new IdentityRole
            {
                Id = "732afff8-a944-45bd-aa94-770cd95f060e",
                Name = "student",
                NormalizedName = "STUDENT"
            };
            var teacher = new IdentityRole
            {
                Id = "51244080-0979-4716-a6b4-d9eca69706ab",
                Name = "teacher",
                NormalizedName = "TEACHER"
            };

            var ikPersonel = new IdentityRole
            {
                Id = "545c4c20-cb0d-4525-a2ff-f9abfe520851",
                Name = "ikPersonel",
                NormalizedName = "IKPERSONEL"
            };

            builder.HasData(admin, student, teacher, ikPersonel);
        }
    }
}
