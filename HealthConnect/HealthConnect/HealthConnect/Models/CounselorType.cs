using System.ComponentModel.DataAnnotations;

namespace HealthConnect.Models
{
    public class CounselorType
    {
        [Key]
        public int CounselorTypeId { get; set; }
        [Required]
        public string CounselorTypeName { get; set; }
        [Required]
        public string Description { get; }
    }
}
