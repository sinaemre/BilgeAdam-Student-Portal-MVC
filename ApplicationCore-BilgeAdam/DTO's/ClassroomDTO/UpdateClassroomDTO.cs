using ApplicationCore_BilgeAdam.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore_BilgeAdam.DTO_s.ClassroomDTO
{
    public class UpdateClassroomDTO
    {
        public int Id { get; set; }

        [Display(Name = "Sınıf NO")]
        public byte? ClassroomNo { get; set; }

        [Display(Name = "Öğretmen")]
        public int? TeacherId { get; set; }

        public List<Teacher>? Teachers { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
