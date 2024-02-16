using System.ComponentModel.DataAnnotations;

namespace NUProjectDemo.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Username required")]
        [Display(Name = "Email")]
        public string email { get; set; }
        [Required(ErrorMessage = "Password required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }
    }
}
