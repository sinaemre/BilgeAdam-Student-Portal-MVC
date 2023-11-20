using ApplicationCore_BilgeAdam.DTO_s.TeacherDTO;
using ApplicationCore_BilgeAdam.Entities.Concrete;
using AutoMapper;
using Infrastructure_BilgeAdam.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WEB_BilgeAdam.Models.ViewModels;

namespace WEB_BilgeAdam.Controllers
{
    public class TeachersController : Controller
    {
        private readonly ITeacherRepository _teacherRepo;
        private readonly IMapper _mapper;

        public TeachersController(ITeacherRepository teacherRepo, IMapper mapper)
        {
            _teacherRepo = teacherRepo;
            _mapper = mapper;
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

        public IActionResult CreateTeacher() => View();

        [HttpPost]
        public async Task<IActionResult> CreateTeacher(CreateTeacherDTO model)
        {
            if (ModelState.IsValid)
            {
                var teacher = _mapper.Map<Teacher>(model);
                await _teacherRepo.AddAsync(teacher);
                TempData["Success"] = $"{teacher.FirstName} {teacher.LastName} öğretmeni sisteme kayıt edilmiştir!";
                return RedirectToAction("Index");
            }
            TempData["Warning"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }
    }
}
