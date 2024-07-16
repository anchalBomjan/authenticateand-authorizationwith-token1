using System.ComponentModel.DataAnnotations;

namespace authenticateand_authorizationwith_token1.Models.DTO
{
    public class LoginDto
    {
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

    }
}
