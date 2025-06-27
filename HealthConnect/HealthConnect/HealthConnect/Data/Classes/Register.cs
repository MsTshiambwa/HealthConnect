using System.ComponentModel.DataAnnotations;

namespace HealthConnect.Data.Classes
{
    public class Register
    {
        [Required]
        [Display(Name = "First Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Role")]
        public string Role { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [Display(Name = "ID Number (South African)")]
        [MaxLength(13)]
        public string IDNumber { get; set; }
        [Required]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Password do not match")]
        public string ConfirmPassword { get; set; }
        public string Gender { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }
        
        [Required]
        [Display(Name = "Area Code")]
        public string AreaCode { get; set; }
        public string Department { get; set; }
        public string IsActive { get; set; }
    }
}
