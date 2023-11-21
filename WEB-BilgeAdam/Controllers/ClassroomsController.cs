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
        private readonly ITeacherRepository _teacherRepo;

        public ClassroomsController(IClassroomRepository classroomRepo, IMapper mapper, ITeacherRepository teacherRepo)
        {
            _classroomRepo = classroomRepo;
            _mapper = mapper;
            _teacherRepo = teacherRepo;
        }
        public async Task<IActionResult> CreateClassroom()
        {
            var model = new CreateClassroomDTO
            {
                Teachers = await _teacherRepo.GetByDefaults(x => x.Status != ApplicationCore_BilgeAdam.Entities.Abstract.Status.Passive)
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateClassroom(CreateClassroomDTO model)
        {
            model.Teachers = await _teacherRepo.GetByDefaults(x => x.Status != ApplicationCore_BilgeAdam.Entities.Abstract.Status.Passive);


            if (ModelState.IsValid)
            {
                if (await _classroomRepo.Any(x => x.ClassroomNo == model.ClassroomNo && x.Status != ApplicationCore_BilgeAdam.Entities.Abstract.Status.Passive))
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

        public async Task<IActionResult> UpdateClassroom(int id)
        {
            if (id > 0)
            {
                var classroom = await _classroomRepo.GetById(id);
                if (classroom != null)
                {
                    var model = _mapper.Map<UpdateClassroomDTO>(classroom);
                    model.Teachers = await _teacherRepo.GetByDefaults(x => x.Status != ApplicationCore_BilgeAdam.Entities.Abstract.Status.Passive);
                    return View(model);
                }
            }
            TempData["Error"] = "Sınıf bulunamadı!";
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateClassroom(UpdateClassroomDTO model)
        {
            model.Teachers = await _teacherRepo.GetByDefaults(x => x.Status != ApplicationCore_BilgeAdam.Entities.Abstract.Status.Passive);
            if (ModelState.IsValid)
            {
                var entity = await _classroomRepo.GetById(model.Id);
                if (await _classroomRepo.Any(x => x.ClassroomNo == model.ClassroomNo && model.ClassroomNo != entity.ClassroomNo && x.Status != ApplicationCore_BilgeAdam.Entities.Abstract.Status.Passive))
                {
                    TempData["Warning"] = "Lütfen başka bir sınıf kodu seçiniz!";
                    return View(model);
                }
                else
                {
                    var classroom = _mapper.Map<Classroom>(model);
                    await _classroomRepo.UpdateAsync(classroom);
                    TempData["Success"] = "Sınıf güncellendi!";
                    return RedirectToAction("Index", "Home");
                }
            }
            TempData["Warning"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }

        public async Task<IActionResult> DeleteClassroom(int id)
        {
            if (id > 0)
            {
                var classroom = await _classroomRepo.GetById(id);
                if (classroom is not null)
                {
                    await _classroomRepo.DeleteAsync(classroom);
                    TempData["Success"] = "Sınıf silinmiştir!";
                    return RedirectToAction("Index", "Home");
                }
            }
            TempData["Error"] = "Sınıf bulunamamıştır!";
            return RedirectToAction("Index", "Home");
        }
    }
}
