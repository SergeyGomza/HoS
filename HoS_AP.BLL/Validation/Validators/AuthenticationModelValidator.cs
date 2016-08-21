using FluentValidation;
using HoS_AP.BLL.Models;

namespace HoS_AP.BLL.Validation.Validators
{
    internal class AuthenticationValidator : AbstractValidator<AuthenticationModel>
    {
        public AuthenticationValidator()
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}