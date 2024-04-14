using System.ComponentModel.DataAnnotations;

namespace PROJECT.Models
{
    public class ProjectImages
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Client has given permision to show publicly")]
        public bool hasPublicPermision { get; set; }

        [Required]
        [Display(Name = "Project Images: ")]
        public byte[]? Images { get; set; }

        
        public int ProjectId { get; set; }
        public Projects Projects { get; set; }
    }
}
