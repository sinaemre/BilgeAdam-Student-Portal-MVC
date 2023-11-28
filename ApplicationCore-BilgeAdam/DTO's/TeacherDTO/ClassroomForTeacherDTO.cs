using ApplicationCore_BilgeAdam.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore_BilgeAdam.DTO_s.TeacherDTO
{
    public class ClassroomForTeacherDTO
    {
        public string TeacherName { get; set; }
        public string ClassroomName { get; set; }
        public byte ClassroomNo { get; set; }
        public List<Student> Students { get; set; }
    }
}
