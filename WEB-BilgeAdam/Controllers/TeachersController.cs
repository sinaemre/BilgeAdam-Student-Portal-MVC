using ApplicationCore_BilgeAdam.DTO_s.TeacherDTO;
using ApplicationCore_BilgeAdam.Entities.Concrete;
using ApplicationCore_BilgeAdam.Entities.UserEntities.Concrete;
using AutoMapper;
using Infrastructure_BilgeAdam.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_BilgeAdam.Models.ViewModels;

namespace WEB_BilgeAdam.Controllers
{
    public class TeachersController : Controller
    {
        private readonly ITeacherRepository _teacherRepo;
        private readonly IMapper _mapper;
        private readonly IClassroomRepository _classroomRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly IStudentRepository _studentRepo;

        public TeachersController(ITeacherRepository teacherRepo, IMapper mapper, IClassroomRepository classroomRepo, UserManager<AppUser> userManager, IStudentRepository studentRepo)
        {
            _teacherRepo = teacherRepo;
            _mapper = mapper;
            _classroomRepo = classroomRepo;
            _userManager = userManager;
            _studentRepo = studentRepo;
        }


        [Authorize(Roles = "admin,ikPersonel")]
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
                        UpdatedDate = x.UpdatedDate,
                        Email = x.Email
                    },
                    where: x => x.Status != ApplicationCore_BilgeAdam.Entities.Abstract.Status.Passive,
                    orderBy: x => x.OrderByDescending(z => z.CreatedDate)
                );

            return View(teachers);
        }

        [Authorize(Roles = "admin,ikPersonel")]
        public IActionResult CreateTeacher() => View();
        
        [Authorize(Roles = "admin,ikPersonel")]
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
        [Authorize(Roles = "admin,ikPersonel")]
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

       
        [Authorize(Roles = "admin,ikPersonel")]
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

        [Authorize(Roles = "admin,ikPersonel")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            if (id > 0)
            {
                var teacher = await _teacherRepo.GetById(id);
                if (teacher is not null) 
                {
                    if (!await _classroomRepo.Any(x => x.TeacherId == teacher.Id && x.Status != ApplicationCore_BilgeAdam.Entities.Abstract.Status.Passive))
                    {
                        await _teacherRepo.DeleteAsync(teacher);
                        TempData["Success"] = $"{teacher.FirstName} {teacher.LastName} silinmiştir!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Warning"] = $"{teacher.FirstName} {teacher.LastName} sınıflarda kayıtlıdır. Önce kayıtlı olduğu sınıflardan çıkartınız!";
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            TempData["Error"] = "Öğretmen bulunamamıştır!";
            return RedirectToAction("Index");
        }


        [Authorize(Roles = "admin,ikPersonel,teacher")]
        public async Task<IActionResult> ShowClassrooms(string userName)
        {
            //Kullancıyı giriş bilgilerinde yakaladık.
            var appUser = await _userManager.FindByNameAsync(userName);

            //Eğitmeni giriş yapan kullanıcının mail'inden yakaladık.
            var teacher = await _teacherRepo.GetByDefault(x => x.Email == appUser.Email);

            //Sınıfı ise giriş yapan kullanıcının mail'inden yakaladığımız eğitmenin ID bilgisinden yakaladık.
            var classrooms = await _classroomRepo.GetFilteredList(select: x => new ClassroomsForTeacherVM
            {
                ClassroomName = x.ClassroomName,
                ClassroomId = x.Id,
                ClassroomNo = x.ClassroomNo,
                ClassroomSize = x.Students.Count,
                TeacherName = x.Teacher.FirstName + " " + x.Teacher.LastName
            },
            where: x => x.Status != ApplicationCore_BilgeAdam.Entities.Abstract.Status.Passive && x.TeacherId == teacher.Id,
            join: x => x.Include(z => z.Students).Include(z => z.Teacher)
            );


            return View(classrooms);
        }

        [Authorize(Roles = "admin,ikPersonel,teacher")]

        public async Task<IActionResult> ShowClassroomForTeacher(int id)
        {
            //Kullancıyı giriş bilgilerinde yakaladık.
            var appUser = await _userManager.FindByNameAsync(User.Identity.Name);

            //Eğitmeni giriş yapan kullanıcının mail'inden yakaladık.
            var teacher = await _teacherRepo.GetByDefault(x => x.Email == appUser.Email);

            //Sınıfı ise giriş yapan kullanıcının mail'inden yakaladığımız eğitmenin ID bilgisinden yakaladık.
            var classroom = await _classroomRepo.GetByDefault(x => x.Id == id);

            var students = await _studentRepo.GetByDefaults(x => x.ClassroomId == classroom.Id);

            var model = new ClassroomForTeacherDTO
            {
                TeacherName = teacher.FirstName + " " + teacher.LastName,
                ClassroomName = classroom.ClassroomName,
                ClassroomNo = classroom.ClassroomNo,
                Students = students
            };

            return View(model);
        }
    }
}
