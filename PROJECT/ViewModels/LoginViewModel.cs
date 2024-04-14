using System.ComponentModel.DataAnnotations;

namespace PROJECT.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "User Name:")]
        [Required(ErrorMessage = "User Name is Required")]
        public string? UserName { get; set; }

        [Display(Name = "Password:")]
        [Required(ErrorMessage = "Password is Required")]
        public string? Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
