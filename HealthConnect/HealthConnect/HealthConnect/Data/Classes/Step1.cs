using System.ComponentModel.DataAnnotations;

namespace HealthConnect.Data.Classes
{
    public class Step1
    {
        [Required]
        [Display(Name = "Do you have bruises, cuts, burns, or other physical injuries?")]
        public int Question1 { get; set; }
        [Display(Name = "Do you feel as if you have depression, anxiety, fear, or post-traumatic stress disorder (PTSD)?")]
        [Required]

        public int Question2 { get; set; }
        [Display(Name = "Are you isolating yourself from your social support network, avoiding family gatherings or social events?")]
        [Required]

        public int Question3 { get; set; }
        [Display(Name = "Are you more anxious, submissive, or overly compliant?")]
        [Required]

        public int Question4 { get; set; }
        [Display(Name = "Is there anyone exerting control over your finances?")]
        [Required]

        public int Question5 { get; set; }
        [Display(Name = "Are you experiencing unwanted sexual advances, coerced sexual acts, or forced sexual activity?")]
        [Required]

        public int Question6 { get; set; }
        [Display(Name = "Is there someone monitoring your whereabouts, isolating you from friends and family, dictating what to wear, or limiting you access to resources or support?")]
        [Required]

        public int Question7 { get; set; }
    }
}
