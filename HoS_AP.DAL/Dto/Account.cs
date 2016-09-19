namespace HoS_AP.DAL.Dto
{
    using System.ComponentModel.DataAnnotations;

    public class Account
    {
        [Key]
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}