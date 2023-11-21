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

        public StudentsController(IStudentRepository studentRepo, IMapper mapper)
        {
            _studentRepo = studentRepo;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var students = await _studentRepo.GetFilteredList
                (
                    select: x => new StudentVM
                    { 
                        Id = x.Id,
                        StudentNo = x.StudentNo,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        BirthDate = x.BirthDate,
                        ClassroomNo = x.Classroom.ClassroomNo,
                        CreatedDate = x.CreatedDate,
                        UpdatedDate = x.UpdatedDate
                    },
                    where: x => x.Status != ApplicationCore_BilgeAdam.Entities.Abstract.Status.Passive,
                    orderBy: x => x.OrderByDescending(z => z.CreatedDate),
                    join: x=> x.Include(z => z.Classroom)
                );

            return View(students);
        }
    }
}
