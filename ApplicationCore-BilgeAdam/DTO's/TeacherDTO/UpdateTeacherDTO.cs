using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore_BilgeAdam.DTO_s.TeacherDTO
{
    public class UpdateTeacherDTO
    {
        public int Id { get; set; }
       
        [Display(Name = "Ad")]
        public string? FirstName { get; set; }

        [Display(Name = "Soyad")]
        public string? LastName { get; set; }

        [Display(Name = "E-Mail")]
        public string? Email { get; set; }

        public DateTime CreatedDate { get; set; }
        
        public DateTime? UpdatedDate { get; set; }
    }
}
