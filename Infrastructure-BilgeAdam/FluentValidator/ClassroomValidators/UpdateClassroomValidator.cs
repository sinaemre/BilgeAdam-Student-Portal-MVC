using ApplicationCore_BilgeAdam.DTO_s.ClassroomDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_BilgeAdam.FluentValidator.ClassroomValidators
{
    public class UpdateClassroomValidator : AbstractValidator<UpdateClassroomDTO>
    {
        public UpdateClassroomValidator()
        {
            RuleFor(x => x.ClassroomNo).NotEmpty().WithMessage("Sınıf NO boş geçilemez!");
            RuleFor(x => x.TeacherId).NotEmpty().WithMessage("Öğretmen boş geçilemez!");
        }
    }
}
