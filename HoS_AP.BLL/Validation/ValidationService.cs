using System.Collections.Generic;
using HoS_AP.BLL.Models;
using HoS_AP.BLL.Validation.Validators;
using HoS_AP.DAL.DaoInterfaces;

namespace HoS_AP.BLL.Validation
{
    internal class ValidationService : IValidationService
    {
        private readonly AuthenticationValidator authenticationValidator;
        private readonly CharacterEditModelValidator characterEditModelValidator;

        public ValidationService(ICharacterDao characterDao)
        {
            authenticationValidator = new AuthenticationValidator();
            characterEditModelValidator = new CharacterEditModelValidator(characterDao);
        }

        ICollection<ValidationError> IValidationService.Validate(AuthenticationModel model)
        {
            return authenticationValidator.Validate(model).Errors.ToValidationResultItem();
        }

        ICollection<ValidationError> IValidationService.Validate(CharacterEditModel model)
        {
            return characterEditModelValidator.Validate(model).Errors.ToValidationResultItem();
        }
    }
}