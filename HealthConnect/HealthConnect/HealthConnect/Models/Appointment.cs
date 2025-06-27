using System.ComponentModel.DataAnnotations;

namespace HealthConnect.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentID { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [Display(Name = "Patient ID Number")]
        public string PatientID { get; set; }
        [Required]
        public string Reason { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
