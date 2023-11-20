using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore_BilgeAdam.DTO_s.ClassroomDTO
{
    public class CreateClassroomDTO
    {
        [Display(Name = "Sınıf NO")]
        public byte ClassroomNo { get; set; }
        
        [Display(Name = "Öğretmen")]
        public int TeacherId { get; set; }
    }
}
