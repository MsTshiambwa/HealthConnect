using System.ComponentModel.DataAnnotations;

namespace HealthConnect.Models
{
    public class ReferralLetter
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Patient ID Number")]
        public string PatientIDNumber { get; set; }
        [Required]
        public string DoctorIDNumber { get; set; }
        [Required]
        public string CounsellingType { get; set; }

        [Display(Name = "Special Remarks")]
        public string SpecialRemarks { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [Display(Name = "Facility Name")]
        public string FacilityName { get; set; }
    }
}
