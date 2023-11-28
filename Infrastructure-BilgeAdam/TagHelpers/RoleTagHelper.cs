using ApplicationCore_BilgeAdam.Entities.UserEntities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_BilgeAdam.TagHelpers
{
    [HtmlTargetElement("td", Attributes = "user-role")]
    public class RoleTagHelper : TagHelper
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleTagHelper(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HtmlAttributeName("user-role")]
        public string RoleId { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            List<string> names = new List<string>();
            var role = await _roleManager.FindByIdAsync(RoleId);

            if (role != null)
            {
                foreach (var user in _userManager.Users.ToList())
                {
                    if (user != null && await _userManager.IsInRoleAsync(user, role.Name))
                    {
                        names.Add(user.UserName);
                    }
                }
            }

            output.Content.SetContent(names.Count == 0 ? "Bu rolde kullanıcı yok!" : names.Count > 3 ? string.Join(", ", names.Take(3)) + " ..." : string.Join(", ", names));
        }
    }
}
