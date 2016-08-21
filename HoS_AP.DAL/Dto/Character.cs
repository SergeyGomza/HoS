using System;
using HoS_AP.Misc;

namespace HoS_AP.DAL.Dto
{
    public class Character
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CharacterTypes Type { get; set; }
        public DateTime Created { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
    }
}