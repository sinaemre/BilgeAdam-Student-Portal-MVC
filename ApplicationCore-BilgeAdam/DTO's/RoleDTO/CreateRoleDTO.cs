using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore_BilgeAdam.DTO_s.RoleDTO
{
    public class CreateRoleDTO
    {
        [Display(Name = "Rol Adı")]
        public string? Name { get; set; }
    }
}
