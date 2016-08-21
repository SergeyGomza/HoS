using System.Collections.Generic;
using System.Linq;
using HoS_AP.BLL.Models;
using HoS_AP.BLL.ServiceInterfaces;
using HoS_AP.DAL.DaoInterfaces;

namespace HoS_AP.BLL.Services
{
    public class CharacterPresentationService : ICharacterPresentationService
    {
        private readonly ICharacterDao characterDao;

        public CharacterPresentationService(ICharacterDao characterDao)
        {
            this.characterDao = characterDao;
        }

        public ICollection<CharacterListItemModel> List()
        {
            return characterDao.Load().Select(x => new CharacterListItemModel(x)).ToList();
        }

        public CharacterEditModel Load(string name)
        {
            var character = characterDao.Load(name);
            if (character == null) return null;
            return new CharacterEditModel(character);
        }
    }
}