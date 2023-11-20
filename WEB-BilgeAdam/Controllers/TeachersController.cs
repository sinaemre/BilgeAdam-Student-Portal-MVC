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

        public async Task<IActionResult> UpdateTeacher(int id)
        {
            if (id > 0)
            {
                var teacher = await _teacherRepo.GetById(id);
                if (teacher is not null)
                {
                    var model = _mapper.Map<UpdateTeacherDTO>(teacher);
                    return View(model);
                }
            }
            TempData["Warning"] = "Öğretmen bulunamadı!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTeacher(UpdateTeacherDTO model)
        {
            if (ModelState.IsValid)
            {
                var teacher = _mapper.Map<Teacher>(model);
                await _teacherRepo.UpdateAsync(teacher);
                TempData["Success"] = $"{model.FirstName} {model.LastName} kişisi güncellendi!";
                return RedirectToAction("Index");
            }
            TempData["Warning"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }

        public async Task<IActionResult> DeleteTeacher(int id)
        {
            if (id > 0)
            {
                var teacher = await _teacherRepo.GetById(id);
                if (teacher is not null) 
                {
                    await _teacherRepo.DeleteAsync(teacher);
                    TempData["Success"] = "Öğretmen silinmiştir!";
                    return RedirectToAction("Index");
                }
            }
            TempData["Error"] = "Öğretmen bulunamamıştır!";
            return RedirectToAction("Index");
        }
    }
}
