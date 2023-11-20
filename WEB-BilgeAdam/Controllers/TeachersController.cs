using Infrastructure_BilgeAdam.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WEB_BilgeAdam.Models.ViewModels;

namespace WEB_BilgeAdam.Controllers
{
    public class TeachersController : Controller
    {
        private readonly ITeacherRepository _teacherRepo;

        public TeachersController(ITeacherRepository teacherRepo)
        {
            _teacherRepo = teacherRepo;
        }

        public async Task<IActionResult> Index()
        {
            var teachers = await _teacherRepo.GetFilteredList
                (
                    select: x => new TeacherVM
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        CreatedDate = x.CreatedDate,
                        UpdatedDate = x.UpdatedDate
                    },
                    where: x => x.Status != ApplicationCore_BilgeAdam.Entities.Abstract.Status.Passive,
                    orderBy: x => x.OrderByDescending(z => z.CreatedDate)
                );

            return View(teachers);
        }
    }
}
