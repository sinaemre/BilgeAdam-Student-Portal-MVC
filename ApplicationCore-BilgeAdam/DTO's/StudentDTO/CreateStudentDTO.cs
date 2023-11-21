using ApplicationCore_BilgeAdam.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore_BilgeAdam.DTO_s.StudentDTO
{
    public class CreateStudentDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? ClassroomId { get; set; }
        public List<Classroom>? Classrooms { get; set; }
    }
}
