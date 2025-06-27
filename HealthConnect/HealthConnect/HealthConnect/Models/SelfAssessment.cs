using System.ComponentModel.DataAnnotations;

namespace HealthConnect.Models
{
    public class SelfAssessment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Result { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
