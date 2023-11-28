using ApplicationCore_BilgeAdam.Entities.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public string? Email { get; set; }

        public DateTime BirthDate { get; set; }

        public double? Exam1 { get; set; }
        public double? Exam2 { get; set; }
        public double? ProjectExam { get; set; }
        public double? Average { get 
            {
                return (Exam1 + Exam2 + ProjectExam) / 3;
            }}
        public string? ProjectPath { get; set; }

        [NotMapped]
        public IFormFile? Project { get; set; }


        public int ClassroomId { get; set; }
        public virtual Classroom Classroom { get; set; } //LazyLoading
    }
}
