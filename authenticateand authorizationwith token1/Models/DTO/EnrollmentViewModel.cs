using System.ComponentModel.DataAnnotations;

namespace authenticateand_authorizationwith_token1.Models.DTO
{
    public class EnrollmentViewModel
    {
        [Key]
        public int EnrollmentID { get; set; }

        [Required]
        [StringLength(20)]
        public string StudentID { get; set; }

        [Required]
        public int CourseID { get; set; }

        [Required]
        public DateTime EnrollmentDate { get; set; }

    }
}
