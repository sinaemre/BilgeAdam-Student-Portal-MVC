using ApplicationCore_BilgeAdam.DTO_s.AccountDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_BilgeAdam.FluentValidator.AccountValidators
{
    public class LoginValidator : AbstractValidator<LoginDTO>
    {
        public LoginValidator()
        {
            RuleFor(x => x.UserName)
               .NotEmpty()
               .WithMessage("Kullanıcı adı boş geçilemez!")
               .MinimumLength(3)
               .WithMessage("En az 3 karakter girmelisiniz");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Şifre boş geçilemez!");
        }
    }
}
