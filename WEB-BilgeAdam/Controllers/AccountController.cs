﻿using ApplicationCore_BilgeAdam.DTO_s.AccountDTO;
using ApplicationCore_BilgeAdam.Entities.Concrete;
using ApplicationCore_BilgeAdam.Entities.UserEntities.Concrete;
using AutoMapper;
using Infrastructure_BilgeAdam.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;

namespace WEB_BilgeAdam.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IPasswordHasher<AppUser> _passwordHasher;
        private readonly IStudentRepository _studentRepo;
        private readonly ITeacherRepository _teacherRepo;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IPasswordHasher<AppUser> passwordHasher, IStudentRepository studentRepo, ITeacherRepository teacherRepo)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _passwordHasher = passwordHasher;
            _studentRepo = studentRepo;
            _teacherRepo = teacherRepo;
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
                        if (await _userManager.IsInRoleAsync(appUser, "Student"))
                        {
                            var student = await _studentRepo.GetByDefault(x => x.Email == appUser.Email);
                            if (student is not null)
                            {
                                TempData["Success"] = $"Hoşgeldin => {student.FirstName + " " + student.LastName}";
                                return RedirectToAction("ShowStudentClassroom", "Students", new { userName = appUser.UserName });
                            }
                        }
                        else if (await _userManager.IsInRoleAsync(appUser, "Teacher"))
                        {
                            var teacher = await _teacherRepo.GetByDefault(x => x.Email == appUser.Email);
                            if (teacher is not null)
                            {
                                TempData["Success"] = $"Hoşgeldin => {teacher.FirstName + " " + teacher.LastName}";
                                return RedirectToAction("ShowClassrooms", "Teachers", new { userName = appUser.UserName });
                            }
                        }
                        TempData["Success"] = $"Hoşgeldin => {appUser.UserName}";
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            TempData["Warning"] = "Kullanıcı adı veya şifre yanlış tekrar deneyin!";
            return View(model);
        }

        public async Task<IActionResult> Edit()
        {
            //User => Bu property giriş yapmış kullanıcının bilgilerine sahiptir. User.Identity.Name bize giriş yağpmış kullanıcının kullanıcı adını verir.
            //var appUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var userId = _userManager.GetUserId(HttpContext.User);
            var appUser = await _userManager.FindByIdAsync(userId);
            var model = new EditUserDTO(appUser);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserDTO model)
        {
            if (ModelState.IsValid)
            {
                //var appUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                var userId = _userManager.GetUserId(HttpContext.User);
                var appUser = await _userManager.FindByIdAsync(userId);
                appUser.UserName = model.UserName;
                appUser.Email = model.Email;
                if (model.Password != null)
                {
                    appUser.PasswordHash = _passwordHasher.HashPassword(appUser, model.Password);
                }
                
                var result = await _userManager.UpdateAsync(appUser);
                if (result.Succeeded)
                {
                    TempData["Success"] = "Profiliniz güncellendi! Tekrar giriş yapınız!";
                    //return RedirectToAction("LogOut");

                }
                else
                {
                    TempData["Error"] = "Profiliniz güncellenemedi!";
                }
            }
            return View(model);
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            TempData["Warning"] = "Başarılı bir şekilde çıkış yaptınız!";
            return RedirectToAction("Login");
        }
    }
}
