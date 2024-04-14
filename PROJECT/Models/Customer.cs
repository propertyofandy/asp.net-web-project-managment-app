using System.ComponentModel.DataAnnotations;
using PROJECT.Validators;
namespace PROJECT.Models
{
    public class Customer
    {
        [Required(ErrorMessage ="First name is required")]
        [Display(Name="First Name:")]
        public string FirstName   { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "Last Name:")]
        public string LastName    { get; set; }

        // added seperate error messages for seperate cases 
        // custom validator

        [StringLength( 255, MinimumLength = 2, ErrorMessage = "invalid address")]
        [Display(Name = "Address:")]
        public string Address     { get; set; }

        [Phone(ErrorMessage = "invalid phone number")]
        [Display(Name = "Phone Number:")]
        public string PhoneNumber { get; set; }

        
        public int Id { get; set; }


        //required default constructer for Post request from form
        public Customer() 
        {
            FirstName = LastName = Address = PhoneNumber = "";
        }

        public virtual ICollection<Projects> Projects { get; set; }

        
    }
}
