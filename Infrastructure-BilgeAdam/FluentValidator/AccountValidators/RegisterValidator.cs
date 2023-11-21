using ApplicationCore_BilgeAdam.DTO_s.AccountDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_BilgeAdam.FluentValidator.AccountValidators
{
    public class RegisterValidator : AbstractValidator<RegisterDTO>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("Kullanıcı adı boş geçilemez!")
                .MinimumLength(3)
                .WithMessage("En az 3 karakter girmelisiniz");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("E-mail boş geçilemez!")
                .EmailAddress()
                .WithMessage("Doğru formatta bir mail girşi yapınız!");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Şifre boş geçilemez!");

                //İsterseniz güçlü şifre kuralı ekleyebilirsiniz!
                //.Matches(@"[A-Z]+")
                //.WithMessage("Your password must contain at least one uppercase letter.")
                //.Matches(@"[a-z]+")
                //.WithMessage("Your password must contain at least one lowercase letter.")
                //.Matches(@"[0-9]+")
                //.WithMessage("Your password must contain at least one number.")
                //.Matches(@"[\!\?\*\.]*$")
                //.WithMessage("Your password must contain at least one (!? *.).");
        }
    }
}
