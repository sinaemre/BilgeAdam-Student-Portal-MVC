using ApplicationCore_BilgeAdam.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_BilgeAdam.SeedData
{
    public class TeacherSeedData : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasData
                (
                    new Teacher
                    {
                        Id = 1,
                        FirstName = "Sina Emre",
                        LastName = "Teacher",
                        Email = "teacher@test.com",
                    }
                );
        }
    }
}
