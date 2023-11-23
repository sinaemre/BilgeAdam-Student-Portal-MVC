using ApplicationCore_BilgeAdam.DTO_s.RoleDTO;
using ApplicationCore_BilgeAdam.Entities.UserEntities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WEB_BilgeAdam.Areas.Admin.Controllers
{
    [Area("Admin")]
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
            return View(_roleManager.Roles);
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
    }
}
