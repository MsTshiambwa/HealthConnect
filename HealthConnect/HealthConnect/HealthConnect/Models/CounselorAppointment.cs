using System.ComponentModel.DataAnnotations;

namespace HealthConnect.Models
{
    public class CounselorAppointment : Appointment
    {
        [Required]
        [Display(Name = "Type Of Counselling:")]
        public string TypeOfCounselling { get; set; }
        [Required]
        [Display(Name = "Number of people coming:")]
        public int NoPeople { get; set; }
    }
}
