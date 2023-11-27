using ApplicationCore_BilgeAdam.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore_BilgeAdam.Entities.Concrete
{
    public class Teacher : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string? Email { get; set; }

        public List<Classroom> Classrooms { get; set; }
    }
}
