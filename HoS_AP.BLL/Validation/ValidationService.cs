using System.Collections.Generic;
using HoS_AP.BLL.Models;
using HoS_AP.BLL.Validation.Validators;
using HoS_AP.DAL.DaoInterfaces;

namespace HoS_AP.BLL.Validation
{
    public class ValidationService : IValidationService
    {
        private readonly AuthenticationValidator authenticationValidator;
        private readonly CharacterEditModelValidator characterEditModelValidator;

        public ValidationService(ICharacterDao characterDao)
        {
            this.authenticationValidator = new AuthenticationValidator();
            this.characterEditModelValidator = new CharacterEditModelValidator(characterDao);
        }

        public ICollection<ValidationError> Validate(AuthenticationModel model)
        {
            return authenticationValidator.Validate(model).Errors.ToValidationResultItem();
        }

        public ICollection<ValidationError> Validate(CharacterEditModel model)
        {
            return characterEditModelValidator.Validate(model).Errors.ToValidationResultItem();
        }
    }
}