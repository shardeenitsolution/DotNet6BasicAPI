using Microsoft.AspNetCore.Identity;

namespace DotNet6BasicAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            FullName = "";
        }
        public string FullName { get; set; } // initalized using constructor
        public string Address { get; set; } = string.Empty; // initalized inline
    }
}
