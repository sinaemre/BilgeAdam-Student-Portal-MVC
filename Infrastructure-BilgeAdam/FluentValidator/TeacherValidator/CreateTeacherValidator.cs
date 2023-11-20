using ApplicationCore_BilgeAdam.DTO_s.TeacherDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_BilgeAdam.FluentValidator.TeacherValidator
{
    public class CreateTeacherValidator : AbstractValidator<CreateTeacherDTO>
    {
        public CreateTeacherValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("Ad alanı zorunludur!")
                .MinimumLength(3)
                .WithMessage("En az 3 karakter girmelisiniz!")
                .MaximumLength(100)
                .WithMessage("En fazla 100 karakter girebilirsiniz!");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Soyad alanı zorunludur!")
                .MinimumLength(3)
                .WithMessage("En az 3 karakter girmelisiniz!")
                .MaximumLength(100)
                .WithMessage("En fazla 100 karakter girebilirsiniz!");
        }
    }
}
