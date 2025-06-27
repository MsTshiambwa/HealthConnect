using System.ComponentModel.DataAnnotations;

namespace HealthConnect.Data.Classes
{
    public class Step3
    {
        [Required]

        [Display(Name = "Do you frequent and escalating arguments, difficulty expressing thoughts and feelings, or a general breakdown in effective communication between partners?")]
        public int Question1 { get; set; }
        [Display(Name = "Do you feel disconnected or emotionally distant from one another, lacking intimacy, or experiencing a decline in emotional closeness?")]
        [Required]

        public int Question2 { get; set; }
        [Display(Name = "Do you feel suspicion, jealousy, or feelings of betrayal can erode trust within a marriage?")]
        [Required]

        public int Question3 { get; set; }
        [Display(Name = "Do you have a noticeable decrease in sexual activity or a change in the quality of intimacy?")]
        [Required]

        public int Question4 { get; set; }
        [Display(Name = "Are you avoiding conversations or spending less time avoiding conversations or spending less time with your parnter?")]
        [Required]

        public int Question5 { get; set; }
        [Display(Name = "is there frequent arguments or disagreements about money, differing financial priorities, or undisclosed financial issues?")]
        [Required]

        public int Question6 { get; set; }
        [Display(Name = "Is there a pattern of constant criticism, blaming, or defensiveness?")]
        [Required]

        public int Question7 { get; set; }
        [Required]

        [Display(Name = "Is there significant difference in values, goals, or expectations for the future with your partner?")]
        public int Question8 { get; set; }
        [Required]

        [Display(Name = "Are ther lingering resentment or unresolved conflicts that have not been adequately addressed?")]
        public int Question9 { get; set; }
        [Required]

        [Display(Name = "Do you feel like you are no longer friends or that you have lost the emotional connection and companionship that once existed?")]
        public int Question10 { get; set; }
    }
}
