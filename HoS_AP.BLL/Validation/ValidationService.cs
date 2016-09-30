using System.Collections.Generic;
using HoS_AP.BLL.Models;
using HoS_AP.BLL.ServiceInterfaces;
using HoS_AP.BLL.Validation.Validators;
using HoS_AP.DAL.DaoInterfaces;

namespace HoS_AP.BLL.Validation
{
    internal class ValidationService : IValidationService
    {
        private readonly IValidationMessageProvider validationMessageProvider;
        private readonly ICharacterDao characterDao;

        public ValidationService(ICharacterDao characterDao, IValidationMessageProvider validationMessageProvider)
        {
            this.characterDao = characterDao;
            this.validationMessageProvider = validationMessageProvider;
        }

        ICollection<ValidationError> IValidationService.Validate(AuthenticationModel model)
        {
            return new AuthenticationValidator(validationMessageProvider).Validate(model).Errors.ToValidationResultItem();
        }

        ICollection<ValidationError> IValidationService.Validate(CharacterEditModel model)
        {
            return new CharacterEditModelValidator(characterDao, validationMessageProvider).Validate(model).Errors.ToValidationResultItem();
        }
    }
}