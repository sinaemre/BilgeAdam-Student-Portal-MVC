using ApplicationCore_BilgeAdam.DTO_s.ClassroomDTO;
using ApplicationCore_BilgeAdam.Entities.Concrete;
using AutoMapper;
using Castle.Core.Resource;
using FluentValidation;
using FluentValidation.Results;
using Infrastructure_BilgeAdam.FluentValidator.ClassroomValidators;
using Infrastructure_BilgeAdam.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WEB_BilgeAdam.Controllers
{
    public class ClassroomsController : Controller
    {
        private readonly IClassroomRepository _classroomRepo;
        private readonly IMapper _mapper;

        public ClassroomsController(IClassroomRepository classroomRepo, IMapper mapper)
        {
            _classroomRepo = classroomRepo;
            _mapper = mapper;
        }
        public IActionResult CreateClassroom()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateClassroom(CreateClassroomDTO model)
        {
            if (ModelState.IsValid)
            {
                if (await _classroomRepo.Any(x => x.ClassroomNo == model.ClassroomNo))
                {
                    TempData["Warning"] = "Lütfen başka bir sınıf kodu seçiniz!";
                    return View(model);
                }
                else
                {
                    var entity = _mapper.Map<Classroom>(model);
                    await _classroomRepo.AddAsync(entity);
                    TempData["Success"] = "Sınıf eklendi!";
                    return RedirectToAction("Index", "Home");
                }
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }
    }
}
