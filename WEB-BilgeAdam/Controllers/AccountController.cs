using ApplicationCore_BilgeAdam.DTO_s.AccountDTO;
using ApplicationCore_BilgeAdam.Entities.UserEntities.Concrete;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WEB_BilgeAdam.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IPasswordHasher<AppUser> _passwordHasher;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IPasswordHasher<AppUser> passwordHasher)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _passwordHasher = passwordHasher;
        }

        //AllowAnonymous => Giriş yapmayan kişiler bu sayayı görüntüleyebilir!
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        //ValidateAntiForgeryToken => siteye yapılan saldırıları engellemek ve kullanıcı bilgilerini korumak için kullanılır.
        [AllowAnonymous, HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            if (ModelState.IsValid)
            {
                var appUser = new AppUser { Email = model.Email, UserName = model.UserName };
                appUser.PasswordHash = _passwordHasher.HashPassword(appUser, model.Password);
                IdentityResult result = await _userManager.CreateAsync(appUser);

                if (result.Succeeded)
                {
                    TempData["Success"] = "Kayıt başarılı. Giriş yapabilirsiniz!";
                    return RedirectToAction("Login");
                }
                else
                {
                    TempData["Error"] = "Kayıt yapılamadı!";
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            TempData["Warning"] = "Lütfen kayıt oluşturma kurallarına uyunuz!";
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            if (ModelState.IsValid)
            {
                var appUser = await _userManager.FindByNameAsync(model.UserName);
                if (appUser != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(appUser.UserName, model.Password, false, false);

                    if (signInResult.Succeeded)
                    {
                        TempData["Success"] = $"Hoşgeldin => {appUser.UserName}";
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            TempData["Warning"] = "Kullanıcı adı veya şifre yanlış tekrar deneyin!";
            return View(model);
        }
    }
}
