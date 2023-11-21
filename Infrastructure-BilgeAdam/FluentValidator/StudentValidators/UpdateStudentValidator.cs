using ApplicationCore_BilgeAdam.DTO_s.StudentDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infrastructure_BilgeAdam.FluentValidator.StudentValidators
{
    public class UpdateStudentValidator : AbstractValidator<UpdateStudentDTO>
    {
        public UpdateStudentValidator()
        {
            Regex regEx = new Regex("^[a-zA-Z- ğüşöçİĞÜŞÖÇ]*$");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("Ad alanı zorunludur!")
                .MinimumLength(3)
                .WithMessage("En az 3 karakter girmelisiniz!")
                .MaximumLength(100)
                .WithMessage("En fazla 100 karakter girebilirsiniz!")
                .Matches(regEx)
                .WithMessage("Sadece harf girilebilir!");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Soyad alanı zorunludur!")
                .MinimumLength(3)
                .WithMessage("En az 3 karakter girmelisiniz!")
                .MaximumLength(100)
                .WithMessage("En fazla 100 karakter girebilirsiniz!")
                .Matches(regEx)
                .WithMessage("Sadece harf girilebilir!");

            RuleFor(x => x.BirthDate)
                .NotEmpty()
                .WithMessage("Doğum Tarihi zorunludur!");

            RuleFor(x => x.ClassroomId)
                .NotEmpty()
                .WithMessage("Sınıf seçimi zorunludur!");
        }
    }
}
