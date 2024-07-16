using System.ComponentModel.DataAnnotations;

namespace authenticateand_authorizationwith_token1.Models.DTO
{
    public class UpdatePermissionDto
    {
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }

    }
}
