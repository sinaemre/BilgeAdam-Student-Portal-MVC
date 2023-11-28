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
    public class IdentityUserRoleSeedData : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData
                (
                    new IdentityUserRole<string>
                    {
                        UserId = "e3569982-6bcc-49ed-b599-6b67ae72d134",
                        RoleId = "2941c96f-580d-4fa1-a18f-80dc158b28cb"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "cea12c00-6fae-4243-ae02-e9498d11a3e5",
                        RoleId = "732afff8-a944-45bd-aa94-770cd95f060e"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "c0333990-76bc-41c7-9b36-f4b99b6aa33d",
                        RoleId = "732afff8-a944-45bd-aa94-770cd95f060e"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "d7e6ce89-4fdb-4dab-b53f-0d55874de67b",
                        RoleId = "51244080-0979-4716-a6b4-d9eca69706ab"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "b83692b7-aaa5-41c9-a97b-5123da9f6eb0",
                        RoleId = "545c4c20-cb0d-4525-a2ff-f9abfe520851"
                    }
                );
        }
    }
}
