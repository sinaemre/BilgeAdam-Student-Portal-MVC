using ApplicationCore_BilgeAdam.DTO_s.RoleDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_BilgeAdam.FluentValidator.RoleValidators
{
    public class CreateRoleValidator : AbstractValidator<CreateRoleDTO>
    {
        public CreateRoleValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Rol adı alanı zorunludur!")
                .MinimumLength(3)
                .WithMessage("En az 3 karakter girmelisiniz!");
        }
    }
}
