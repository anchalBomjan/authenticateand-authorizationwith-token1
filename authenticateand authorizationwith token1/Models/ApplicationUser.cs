using Microsoft.AspNetCore.Identity;

namespace authenticateand_authorizationwith_token1.Models
{
    public class ApplicationUser:IdentityUser
    {  // Add additional properties here
    
        public string FirstName { get; set; }

        public string LastName { get; set; }
      
       // public string Email { get; set; }


    }
}
