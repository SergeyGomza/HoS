namespace HoS_AP.BLL.Models
{
    using System.ComponentModel.DataAnnotations;

    public class AuthenticationModel
    {
        [Display(Name = "UserName", ResourceType = typeof(Resources.Resource))]
        public string UserName { get; set; }

        [Display(Name = "Password", ResourceType = typeof(Resources.Resource))]
        public string Password { get; set; }
    }
}