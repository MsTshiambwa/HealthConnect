using System.ComponentModel.DataAnnotations;

namespace HealthConnect.Models
{
    public class SessionType
    {
        [Key]
        public int CounsellingSessionId { get; set; }
        [Required]
        public string DoctorName { get; set; }
        [Required]
        public string DoctorID { get; set; }
        [Required]
        public string PatientName { get; set; }
        [Required]
        [Display(Name = "Patient ID")]
        public string PatientID { get; set; }
        [Required]
        [Display(Name = "Type Of Counselling")]
        public string CounsellingType { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Feedback { get; set; }
    }
}
