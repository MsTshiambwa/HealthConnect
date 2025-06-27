using System.ComponentModel.DataAnnotations;

namespace HealthConnect.Models
{
    public class DoctorCounselor
    {
        [Key]
        public int CouncilID { get; set; }
        [Required]
        public string PatientNumberID { get; set; }
        [Required]
        public string Recommendations { get; set; }
        [Required]
        public string TypeOfCounselling { get; set; }
        [Required]
        public string NotesFromMeeting { get; set; }
        [Required]
        public string DoctorID { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
    

