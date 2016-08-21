using System;
using System.Collections.Generic;
using System.Linq;
using HoS_AP.DAL.DaoInterfaces;
using HoS_AP.DAL.Dto;

namespace HoS_AP.DAL.Dao
{
    internal class CharacterDao : FileSystemRepository, ICharacterDao
    {
        ICollection<Character> ICharacterDao.Load()
        {
            return Characters.ToList();
        }

        Character ICharacterDao.Load(string name)
        {
            return Characters.FirstOrDefault(x => x.Name == name);
        }

        void ICharacterDao.Save(Character character)
        {
            base.Save(character);
        }

        Character ICharacterDao.Load(Guid Id)
        {
            return Characters.FirstOrDefault(x => x.Id == Id);
        }
    }
}