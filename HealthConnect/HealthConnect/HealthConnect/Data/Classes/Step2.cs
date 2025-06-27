using System.ComponentModel.DataAnnotations;

namespace HealthConnect.Data.Classes
{
    public class Step2
    {
        [Required]
        [Display(Name = "Do you feel sad, empty, or down most of the time, often without a clear reason?")]
        public int Question1 { get; set; }
        [Display(Name = "Are you losing interest in activities or hobbies that were previously enjoyed, finding it difficult to experience joy or pleasure?")]
        [Required]

        public int Question2 { get; set; }
        [Display(Name = "Are you seeing significant changes in appetite, resulting in weight loss or weight gain?")]
        [Required]

        public int Question3 { get; set; }
        [Display(Name = "Do you feel as if you have insomnia (difficulty falling asleep, staying asleep, or early morning awakening) or hypersomnia (excessive sleeping) can be indicators of depression?")]
        [Required]

        public int Question4 { get; set; }
        [Display(Name = "Are you constantly feeling tired, lacking energy, or experiencing a general sense of weakness?")]
        [Required]

        public int Question5 { get; set; }
        [Display(Name = "Are you struggling to focus, make decisions, or remember things?")]
        [Required]

        public int Question6 { get; set; }
        [Display(Name = "Are you experiencing excessive guilt or feelings of worthlessness, self-blame, or feeling like a burden to others?")]
        [Required]

        public int Question7 { get; set; }
        [Required]


        [Display(Name = "Do you have frequent thoughts about death, dying, or suicide, or even making plans for suicide?")]
        public int Question8 { get; set; }
        [Required]

        [Display(Name = "Do you feeling agitated, irritable, or easily frustrated, even over small matters?")]
        public int Question9 { get; set; }
        [Required]

        [Display(Name = "Are you avoiding social interactions, isolating oneself from friends and family, or losing interest in maintaining relationships?")]
        public int Question10 { get; set; }
        [Required]

        [Display(Name = "Do you get headaches, stomachaches, and other unexplained aches or pains?")]

        public int Question11 { get; set; }
    }
}
