using ApplicationCore_BilgeAdam.Entities.Concrete;
using Infrastructure_BilgeAdam.Context;
using Infrastructure_BilgeAdam.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_BilgeAdam.Services.Concrete
{
    public class ClassroomRepository : BaseRepository<Classroom>, IClassroomRepository
    {
        public ClassroomRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
