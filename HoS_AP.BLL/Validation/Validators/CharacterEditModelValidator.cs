using FluentValidation;
using HoS_AP.BLL.Models;
using HoS_AP.DAL.DaoInterfaces;
using HoS_AP.Misc;

namespace HoS_AP.BLL.Validation.Validators
{
    internal class CharacterEditModelValidator : AbstractValidator<CharacterEditModel>
    {
        private readonly ICharacterDao characterDao;

        public CharacterEditModelValidator(ICharacterDao characterDao)
        {
            this.characterDao = characterDao;
            RuleFor(x => x.Name).NotEmpty()
                .Matches("^[a-zA-Z ]+$")
                    .WithMessage("Please use only english letters and space")
                .Must((model, name) => BeUnique(model))
                    .WithMessage("Sorry. Character with such name is already registered in the system");
            RuleFor(x => x.Price)   
                .InclusiveBetween(5, 25).WithMessage("Please set value between 5 and 25");
            RuleFor(x => x.Type).NotEqual(CharacterTypes.None);
        }

        private bool BeUnique(CharacterEditModel model)
        {
            var character = characterDao.Load(model.Name);
            return character == null || character.Id == model.Id;
        }
    }
}