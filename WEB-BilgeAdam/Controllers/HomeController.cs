using Infrastructure_BilgeAdam.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WEB_BilgeAdam.Models;
using WEB_BilgeAdam.Models.ViewModels;

namespace WEB_BilgeAdam.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IClassroomRepository _classroomRepo;

        public HomeController(ILogger<HomeController> logger, IClassroomRepository classroomRepo)
        {
            _logger = logger;
            _classroomRepo = classroomRepo;
        }

        public async Task<IActionResult> Index()
        {
            var classrooms = await _classroomRepo.GetFilteredList 
                (
                    select: x => new ClassroomVM
                    {
                        Id = x.Id,
                        ClassroomNO = x.ClassroomNo,
                        ClassSize = x.Students.Where(x => x.Status != ApplicationCore_BilgeAdam.Entities.Abstract.Status.Passive).ToList().Count,
                        TeacherName = x.Teacher.FirstName + " " + x.Teacher.LastName,
                        TeacherId = x.Teacher.Id
                    },
                    where: x => x.Status != ApplicationCore_BilgeAdam.Entities.Abstract.Status.Passive,
                    orderBy: x => x.OrderByDescending(z => z.CreatedDate),
                    join: x => x.Include(z => z.Teacher).Include(z => z.Students)
                );

            return View(classrooms);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}