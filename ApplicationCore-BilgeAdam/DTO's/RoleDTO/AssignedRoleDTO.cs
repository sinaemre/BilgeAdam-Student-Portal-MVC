using ApplicationCore_BilgeAdam.Entities.UserEntities.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore_BilgeAdam.DTO_s.RoleDTO
{
    public class AssignedRoleDTO
    {
        public IdentityRole Role { get; set; }
        public string? RoleName { get; set; }

        public List<AppUser>? HasRole { get; set; }
        public List<AppUser>? HasNotRole { get; set; }

        public string[]? AddIds { get; set; }
        public string[]? DeleteIds { get; set; }

    }
}
