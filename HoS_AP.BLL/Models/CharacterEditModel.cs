using System;
using System.Collections.Generic;
using System.Linq;
using HoS_AP.DAL.Dto;
using HoS_AP.Misc;

namespace HoS_AP.BLL.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CharacterEditModel
    {
        public CharacterEditModel()
        {
        }

        public CharacterEditModel(Character character)
        {
            Id = character.Id;
            Name = character.Name;
            Type = character.Type;
            Price = character.Price;
            Active = character.Active;
            Deleted = character.Deleted;
        }

        public Guid Id { get; set; }

        [Display(Name = "CharacterName", ResourceType = typeof(Resources.Resource))]
        public string Name { get; set; }

        [Display(Name = "CharacterType", ResourceType = typeof(Resources.Resource))]
        public CharacterTypes Type { get; set; }

        [Display(Name = "CharacterPrice", ResourceType = typeof(Resources.Resource))]
        public decimal Price { get; set; }

        [Display(Name = "CharacterActive", ResourceType = typeof(Resources.Resource))]
        public bool Active { get; set; }

        public bool Deleted { get; set; }

        public IEnumerable<CharacterTypes> Types
        {
            get
            {
                return Enum.GetValues(typeof(CharacterTypes)).Cast<CharacterTypes>().Where(x=> x != CharacterTypes.None);
            }
        }

        public bool DisplayDeleteButton
        {
            get { return Id != Guid.Empty && !Deleted; }
        }

        public bool DisplayRecoverButton
        {
            get { return Id != Guid.Empty && Deleted; }
        }
    }
}