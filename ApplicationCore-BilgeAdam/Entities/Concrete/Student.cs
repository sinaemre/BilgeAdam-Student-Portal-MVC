using ApplicationCore_BilgeAdam.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore_BilgeAdam.Entities.Concrete
{
    public class Student : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string FirstName { get; set; }
        
        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public int ClassroomId { get; set; }
        public virtual Classroom Classroom { get; set; } //LazyLoading
    }
}
