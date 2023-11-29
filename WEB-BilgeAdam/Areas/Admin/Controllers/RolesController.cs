using ApplicationCore_BilgeAdam.DTO_s.RoleDTO;
using ApplicationCore_BilgeAdam.Entities.UserEntities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WEB_BilgeAdam.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]

    public class RolesController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View(_roleManager.Roles.ToList());
        }

        public IActionResult CreateRole() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(CreateRoleDTO model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(model.Name));

                if (result.Succeeded)
                {
                    TempData["Success"] = "Rol başarılı bir şekilde eklendi!";
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                        TempData["Error"] = error.Description;
                    }
                    return View(model);
                }
            }
            TempData["Warning"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }

        public async Task<IActionResult> AssignedUser(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            List<AppUser> hasRole = new List<AppUser>();
            List<AppUser> hasNotRole = new List<AppUser>();

            foreach (var user in _userManager.Users.ToList())
            {
                //Uzun Hali
                //if (await _userManager.IsInRoleAsync(user, role.Name))
                //{
                //    hasRole.Add(user);
                //}
                //else
                //{
                //    hasNotRole.Add(user);
                //}

                //Kısa Hali
                var list = await _userManager.IsInRoleAsync(user, role.Name) ? hasRole : hasNotRole;
                list.Add(user);
            }

            AssignedRoleDTO model = new AssignedRoleDTO
            {
                Role = role,
                HasRole = hasRole,
                HasNotRole = hasNotRole,
                RoleName = role.Name
            };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignedUser(AssignedRoleDTO model)
        {
            IdentityResult result;

            foreach (var userId in model.AddIds ?? new string[] { })
            {
                AppUser appUser = await _userManager.FindByIdAsync(userId);
                result = await _userManager.AddToRoleAsync(appUser, model.RoleName);
            }

            foreach (var userId in model.DeleteIds ?? new string[] { })
            {
                AppUser appUser = await _userManager.FindByIdAsync(userId);
                result = await _userManager.RemoveFromRoleAsync(appUser, model.RoleName);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            var hasRole = new List<AppUser>();

            foreach (var user in _userManager.Users.ToList())
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    hasRole.Add(user);
                }
            }
            if (role != null && hasRole.Count == 0)
            {
                await _roleManager.DeleteAsync(role);
                TempData["Success"] = "Rol silinmiştir!";
            }
            else
            {
                TempData["Error"] = "Bu role sahip kullanıcılar var. Silemezsiniz!";
            }
            return RedirectToAction("Index");
        }
    }
}
