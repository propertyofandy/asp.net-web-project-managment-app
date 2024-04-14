using System.ComponentModel.DataAnnotations;
using PROJECT.Validators;

namespace PROJECT.Models
{
    public enum ProjectType { Kitchen, Bathroom, Deck, AddOn }

    public class Projects
    {
        [Display(Name = "Project Type:")]
        public ProjectType ProjType { get; set; }

        
        [Display(Name = "Completed:")]
        public bool IsComplete { get; set; }

        [DateNowValidator(ErrorMessage = "date must be within the last year")]
        [Display(Name = "Date Ordered:")]
        public DateTime OrderedDate { get; set; }

        [BalanceVlidator(ErrorMessage = "cost can not be negative")]
        [Display(Name = "Cost:")]
        public int Cost { get; set; }

        public int Id { get; set; }

        public virtual ICollection<ProjectImages>? Images { get; set; }

        [Display(Name ="Customer Id:")]
        public int CustomerId { get; set; }

        [Display(Name ="Customer:")]
        public Customer Customer { get; set; }
    }
}
