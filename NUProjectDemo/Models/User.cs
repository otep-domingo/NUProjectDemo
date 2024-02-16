using System.ComponentModel.DataAnnotations;

namespace NUProjectDemo.Models
{
    public class User
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage="Username required")]
        [Display(Name = "Email")]
        public string email { get; set; }
        [Required(ErrorMessage = "Password required")]
        [DataType(DataType.Password)]
        [Display(Name ="Password")]
        public string password { get; set; }
        [Display(Name = "Lastname")]
        public string lastname { get; set; }
        [Display(Name = "Firstname")]
        public string  firstname { get; set; }
        [Display(Name = "Course")]
        public string course { get; set; }
    }
}
