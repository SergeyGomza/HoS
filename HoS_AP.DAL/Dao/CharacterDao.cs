using System.Collections.Generic;
using System.Linq;
using HoS_AP.DAL.DaoInterfaces;
using HoS_AP.DAL.Dto;

namespace HoS_AP.DAL.Dao
{
    public class CharacterDao : FileSystemRepository, ICharacterDao
    {
        public ICollection<Character> Load()
        {
            return Characters.ToList();
        }
    }
}