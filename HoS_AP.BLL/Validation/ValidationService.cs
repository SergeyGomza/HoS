using System.Collections.Generic;
using HoS_AP.BLL.Models;
using HoS_AP.BLL.Validation.Validators;

namespace HoS_AP.BLL.Validation
{
    public class ValidationService : IValidationService
    {
        private readonly AuthenticationValidator authenticationValidator = new AuthenticationValidator();

        public ICollection<ValidationError> Validate(AuthenticationModel model)
        {
            return authenticationValidator.Validate(model).Errors.ToValidationResultItem();
        }
    }
}