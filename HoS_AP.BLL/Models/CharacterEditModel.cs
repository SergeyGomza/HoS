using System;
using System.Collections.Generic;
using System.Linq;
using HoS_AP.Misc;

namespace HoS_AP.BLL.Models
{
    public class CharacterEditModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public CharacterTypes Type { get; set; }

        public decimal Price { get; set; }

        public bool Active { get; set; }

        public IEnumerable<CharacterTypes> Types
        {
            get
            {
                return Enum.GetValues(typeof(CharacterTypes)).Cast<CharacterTypes>().Where(x=> x != CharacterTypes.None);
            }
        }
    }
}