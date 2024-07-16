using System.ComponentModel.DataAnnotations;

namespace authenticateand_authorizationwith_token1.Models
{
    public class Student
    {

        [Key]
        [StringLength(20)]
        public string StudentID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
