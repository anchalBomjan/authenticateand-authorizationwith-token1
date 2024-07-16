using System.ComponentModel.DataAnnotations;

namespace authenticateand_authorizationwith_token1.Models
{
    public class Enrollment
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

        // Navigation properties for related data
        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
