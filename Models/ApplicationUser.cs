using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AeonRegistryAPI.Models
{
    public class ApplicationUser: IdentityUser
    {
        [Required]
        public string? FirsName { get; set; }
        [Required]
        public string? LastName { get; set; }

        public string FullName => $"{FirsName} {LastName}";
    }
}
