using System.ComponentModel.DataAnnotations;

namespace PROJECT.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "First Name:")]
        [Required(ErrorMessage = "First Name is Required")]
        public string? FirstName { get; set; }

        [Display(Name = "Last Name:")]
        [Required(ErrorMessage = "Last Name is Required")]
        public string? LastName { get; set; }
        
        [EmailAddress]
        [Display(Name = "Email:")]
        [Required(ErrorMessage = "Email is Required")]
        public string? Email { get; set; }

        [Display(Name = "User Name:")]
        [Required(ErrorMessage = "User Name is Required")]
        public string? UserName { get; set; }

        [Display(Name = "Password:")]
        [Required(ErrorMessage = "Password is Required")]
        public string? Password { get; set; }

        [Display(Name = "Retype Password:")]
        [Required(ErrorMessage = "Password 2 is Required")]
        public string? RePassword { get; set; }

        [Display(Name = "Remember:")]
        public bool RememberMe { get; set; }


    }
}
