using ApplicationCore_BilgeAdam.DTO_s.StudentDTO;
using ApplicationCore_BilgeAdam.Entities.Concrete;
using AutoMapper;
using Infrastructure_BilgeAdam.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_BilgeAdam.Models.ViewModels;

namespace WEB_BilgeAdam.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentRepository _studentRepo;
        private readonly IMapper _mapper;
        private readonly IClassroomRepository _classroomRepo;

        public StudentsController(IStudentRepository studentRepo, IMapper mapper, IClassroomRepository classroomRepo)
        {
            _studentRepo = studentRepo;
            _mapper = mapper;
            _classroomRepo = classroomRepo;
        }

        public async Task<IActionResult> Index()
        {
            var students = await _studentRepo.GetFilteredList
                (
                    select: x => new StudentVM
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        BirthDate = x.BirthDate,
                        ClassroomNo = x.Classroom.ClassroomNo,
                        CreatedDate = x.CreatedDate,
                        UpdatedDate = x.UpdatedDate
                    },
                    where: x => x.Status != ApplicationCore_BilgeAdam.Entities.Abstract.Status.Passive,
                    orderBy: x => x.OrderByDescending(z => z.CreatedDate),
                    join: x => x.Include(z => z.Classroom)
                );

            return View(students);
        }

        public async Task<IActionResult> CreateStudent()
        {
            var model = new CreateStudentDTO
            {
                Classrooms = await _classroomRepo.GetByDefaults(x => x.Status != ApplicationCore_BilgeAdam.Entities.Abstract.Status.Passive)

            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent(CreateStudentDTO model)
        {
            model.Classrooms = await _classroomRepo.GetByDefaults(x => x.Status != ApplicationCore_BilgeAdam.Entities.Abstract.Status.Passive);
            if (ModelState.IsValid)
            {
                var student = _mapper.Map<Student>(model);
                await _studentRepo.AddAsync(student);
                TempData["Success"] = $"{student.FirstName} {student.LastName} öğrencisi sisteme kayıt edilmiştir!";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }

        public async Task<IActionResult> UpdateStudent(int id)
        {
            if (id > 0)
            {
                var student = await _studentRepo.GetById(id);
                if (student != null)
                {
                    var model = _mapper.Map<UpdateStudentDTO>(student);
                    model.Classrooms = await _classroomRepo.GetByDefaults(x => x.Status != ApplicationCore_BilgeAdam.Entities.Abstract.Status.Passive);
                    return View(model);
                }
            }
            TempData["Error"] = "Öğrenci bulunamadı!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStudent(UpdateStudentDTO model)
        {
            model.Classrooms = await _classroomRepo.GetByDefaults(x => x.Status != ApplicationCore_BilgeAdam.Entities.Abstract.Status.Passive);
            if (ModelState.IsValid)
            {
                var student = _mapper.Map<Student>(model);
                await _studentRepo.UpdateAsync(student);
                TempData["Success"] = $"{student.FirstName} {student.LastName} adlı öğrenci güncellenmiştir!";
                return RedirectToAction("Index");
            }
            TempData["Warning"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }

        public async Task<IActionResult> DeleteStudent(int id)
        {
            if (id > 0)
            {
                var student = await _studentRepo.GetById(id);
                if (student != null)
                {
                    await _studentRepo.DeleteAsync(student);
                    TempData["Success"] = "Öğrenci silinmiştir!";
                    return RedirectToAction("Index");
                }
            }
            TempData["Error"] = "Öğrenci bulunamadı!";
            return RedirectToAction("Index");
        }
    }
}
