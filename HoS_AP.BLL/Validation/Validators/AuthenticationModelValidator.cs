using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using HoS_AP.BLL.Models;

namespace HoS_AP.BLL.Validation.Validators
{
    public class AuthenticationValidator : AbstractValidator<AuthenticationModel>
    {
        public AuthenticationValidator()
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}