using ApplicationCore_BilgeAdam.DTO_s.ClassroomDTO;
using ApplicationCore_BilgeAdam.DTO_s.StudentDTO;
using ApplicationCore_BilgeAdam.Entities.Concrete;
using ApplicationCore_BilgeAdam.Entities.UserEntities.Concrete;
using AutoMapper;
using Infrastructure_BilgeAdam.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
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
        private readonly ITeacherRepository _teacherRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public StudentsController(IStudentRepository studentRepo, IMapper mapper, IClassroomRepository classroomRepo, ITeacherRepository teacherRepo, UserManager<AppUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _studentRepo = studentRepo;
            _mapper = mapper;
            _classroomRepo = classroomRepo;
            _teacherRepo = teacherRepo;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
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
                        Email = x.Email,
                        ClassroomName = x.Classroom.ClassroomName,
                        Average = x.Average,
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

        public async Task<IActionResult> ShowStudentClassroom(string userName)
        {
            var appUser = await _userManager.FindByNameAsync(userName);

            var student = await _studentRepo.GetByDefault(x => x.Email == appUser.Email);
            if (student != null)
            {
                var classroom = await _classroomRepo.GetById(student.ClassroomId);
                var teacher = await _teacherRepo.GetById(classroom.TeacherId);
                if (classroom is not null)
                {
                    var model = _mapper.Map<ClassroomStudentDTO>(classroom);
                    model.TeacherName = teacher.FirstName + " " + teacher.LastName;
                    return View(model);
                }
            }
            TempData["Error"] = "Bir şeyler ters gitti!";
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> StudentExams(string userName)
        {
            var appUser = await _userManager.FindByNameAsync(userName);

            var student = await _studentRepo.GetByDefault(x => x.Email == appUser.Email);
            if (student != null)
            {
                var model = new StudentExamVM
                {
                    Exam1 = student.Exam1,
                    Exam2 = student.Exam2,
                    ProjectExam = student.ProjectExam,
                    ProjectPath = student.ProjectPath,
                    Average = student.Average
                };

                return View(model);
            }
            TempData["Error"] = "Öğrenci bulunamadı!";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> SetStudentExams(int id)
        {
            var student = await _studentRepo.GetById(id);
            if (student is not null)
            {
                var model = new StudentSetExamVM
                {
                    Id = student.Id,
                    FullName = student.FirstName + " " + student.LastName,
                    ClassroomId = student.ClassroomId
                };
                return View(model);
            }
            TempData["Error"] = "Öğrenci bulunamadı!";
            return RedirectToAction("ShowClassrooms", "Teachers", new { userName = User.Identity.Name });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> SetStudentExams(StudentSetExamVM model)
        {
            var student = await _studentRepo.GetById(model.Id);
            if (student is not null) 
            {
                if (model.Exam1 != null)
                {
                    student.Exam1 = model.Exam1;
                }
                if (model.Exam2 != null)
                {
                    student.Exam2 = model.Exam2;
                }
                if (model.Project != null)
                {
                    string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "students/projects");
                    string fileName = $"{Guid.NewGuid()}_{student.FirstName}_{student.LastName}_{model.Project.FileName}";
                    string filePath = Path.Combine(uploadDir, fileName);
                    FileStream fileStream = new FileStream(filePath, FileMode.Create);
                    await model.Project.CopyToAsync(fileStream);
                    fileStream.Close();
                    student.ProjectPath = fileName;
                }
                if (model.ProjectExam != null)
                {
                    student.ProjectExam = model.ProjectExam;
                }
                await _studentRepo.UpdateAsync(student);
                TempData["Success"] = "Sınav notları başarılı bir şekilde girilmiştir!";
                return RedirectToAction("ShowClassroomForTeacher", "Teachers", new { id = student.ClassroomId });
            }
            TempData["Error"] = "Öğrenci bulunamadı!";
            return RedirectToAction("ShowClassrooms", "Teachers", new { userName = User.Identity.Name });
        }

        public ActionResult DownloadFile(string filePath)
        {
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "students/projects/");
            byte[] fileBytes = System.IO.File.ReadAllBytes(path + filePath);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, filePath);
        }
    }
}
