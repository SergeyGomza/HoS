using System;
using System.Collections.Generic;

namespace HoS_AP.DAL.EFDao
{
    using System.Data.Entity;
    using System.Linq;

    using HoS_AP.DAL.DaoInterfaces;
    using HoS_AP.DAL.Dto;
    using HoS_AP.DAL.EF;

    public class EFCharacterDao : ICharacterDao
    {
        ICollection<Character> ICharacterDao.Load()
        {
            using (var context = new DatabaseContext())
            {
                return context.Characters.ToList();
            }
        }

        Character ICharacterDao.Load(string name)
        {
            using (var context = new DatabaseContext())
            {
                return context.Characters.FirstOrDefault(c => c.Name == name);
            }
        }

        void ICharacterDao.Save(Character character)
        {
            using (var context = new DatabaseContext())
            {
                var entity = context.Characters.FirstOrDefault(c => c.Id == character.Id);

                if (entity != null)
                {
                    entity.Name = character.Name;
                    entity.Active = character.Active;
                    entity.Created = character.Created;
                    entity.Deleted = character.Deleted;
                    entity.Price = character.Price;
                    entity.Type = character.Type;

                    context.Entry(entity).State = EntityState.Modified;
                }
                else
                {
                    context.Characters.Add(character);
                }

                context.SaveChanges();
            }
        }

        Character ICharacterDao.Load(Guid Id)
        {
            using (var context = new DatabaseContext())
            {
                return context.Characters.FirstOrDefault(x => x.Id == Id);
            }
        }
    }
}
