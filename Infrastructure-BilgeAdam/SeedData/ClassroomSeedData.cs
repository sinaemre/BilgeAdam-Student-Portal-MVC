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
    public class ClassroomSeedData : IEntityTypeConfiguration<Classroom>
    {
        public void Configure(EntityTypeBuilder<Classroom> builder)
        {
            builder.HasData
                (
                    new Classroom 
                    {
                        Id = 1,
                        ClassroomName = "YZL-8147",
                        ClassroomNo = 10,
                        ClassroomDescription = "320 Saat .NET Full Stack Developer Eğitimi",
                        TeacherId = 1
                    }
                );
        }
    }
}
