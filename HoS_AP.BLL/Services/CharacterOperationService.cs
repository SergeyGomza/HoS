using System;
using System.Linq;
using HoS_AP.BLL.Mappers;
using HoS_AP.BLL.Models;
using HoS_AP.BLL.ServiceInterfaces;
using HoS_AP.BLL.Validation;
using HoS_AP.DAL.DaoInterfaces;
using HoS_AP.DAL.Dto;

namespace HoS_AP.BLL.Services
{
    public class CharacterOperationService : ICharacterOperationService
    {
        private readonly IValidationService validationService;
        private readonly ICharacterDao characterDao;

        public CharacterOperationService(IValidationService validationService, ICharacterDao characterDao)
        {
            this.validationService = validationService;
            this.characterDao = characterDao;
        }

        public ValidationResult Save(CharacterEditModel model)
        {
            var validaitonErrors = validationService.Validate(model);
            if (validaitonErrors.Any())
            {
                return new ValidationResult(validaitonErrors);
            }

            var character = model.Id == Guid.Empty 
                ? new Character() : 
                characterDao.Load(model.Id);

            model.MapTo(ref character);
            characterDao.Save(character);

            return ValidationResult.Ok;      
        }

        public void Delete(string name)
        {
            SetDeleted(name, true);
        }

        public void Recover(string name)
        {
            SetDeleted(name, false);
        }

        private void SetDeleted(string name, bool deleted)
        {
            var character = characterDao.Load(name);
            if (character == null) return;
            character.Deleted = deleted;
            characterDao.Save(character);
        }
    }
}