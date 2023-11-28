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
    public class StudentSeedData : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasData
                (
                    new Student
                    {
                        Id = 1,
                        FirstName = "Sina Emre",
                        LastName = "Öğrenci",
                        BirthDate = new DateTime(1996, 01, 23),
                        Email = "student@test.com",
                        ClassroomId = 1
                    },
                    new Student
                    {
                        Id = 2,
                        FirstName = "Test",
                        LastName = "Öğrenci",
                        BirthDate = new DateTime(1999, 05, 23),
                        Email = "student2@test.com",
                        ClassroomId = 1
                    }
                );
        }
    }
}
