using System.ComponentModel.DataAnnotations;

namespace authenticateand_authorizationwith_token1.Models.DTO
{
    public class CourseViewModel
    {
        [Required]
        [StringLength(100)]
        public string CourseName { get; set; }

        public string CourseDescription { get; set; }

        [Required]
        public int Credits { get; set; }
    }
}
