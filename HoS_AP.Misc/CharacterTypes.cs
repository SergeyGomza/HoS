namespace HoS_AP.Misc
{
    using System.ComponentModel.DataAnnotations;

    public enum CharacterTypes
    {
        None = 0,
        [Display(Name = "Character_Assassin", ResourceType = typeof(Resources.Resource))]
        Assassin = 1,
        [Display(Name = "Character_Warrior", ResourceType = typeof(Resources.Resource))]
        Warrior = 2,
        [Display(Name = "Character_Support", ResourceType = typeof(Resources.Resource))]
        Support = 3,
        [Display(Name = "Character_Specialist", ResourceType = typeof(Resources.Resource))]
        Specialist = 4
    }
}