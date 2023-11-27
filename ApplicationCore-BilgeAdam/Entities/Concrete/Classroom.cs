using ApplicationCore_BilgeAdam.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore_BilgeAdam.Entities.Concrete
{
    public class Classroom : BaseEntity
    {
        public byte ClassroomNo { get; set; }
        public string? ClassroomName { get; set; }
        public string? ClassroomDescription { get; set; }

        public int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }

        public List<Student> Students { get; set; }
    }
}
