using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HealthConnect.Models
{
    public class UserDetails : IdentityUser
    {
        [Required]
        [Display(Name = "First Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Role")]
        public string Role { get; set; }
        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }
        [Required]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [Display(Name = "South African ID")]
        [StringLength(13)]
        public string IDNumber { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }
       
        [Required]
        [Display(Name = "Area Code")]
        public string AreaCode { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public string IsActive { get; set; }
    }
}
