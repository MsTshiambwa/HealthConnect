using System.ComponentModel.DataAnnotations;

namespace HealthConnect.Models
{
    public class Counselors : UserDetails
    {
        [Required]
        public int? CounselorType { get; set; }
    }
}
