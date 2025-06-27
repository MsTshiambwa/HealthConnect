using System.ComponentModel.DataAnnotations;

namespace HealthConnect.Data.Classes
{
    public class Step4
    {
        [Required]

        [Display(Name = "Do you have sudden weight loss or gain, bloodshot or glazed eyes, frequent nosebleeds (in the case of cocaine use), tremors or shaky hands, poor personal hygiene, or changes in sleep patterns?")]
        public int Question1 { get; set; }
        [Display(Name = "Are you isolating yourself from family and friends, secretive or lie about your activities, experience sudden mood swings or extreme irritability, displays a lack of motivation or interest in previously enjoyed activities, or a decline in academic or work performance.?")]
        [Required]

        public int Question2 { get; set; }
        [Display(Name = "Do you have money problems, such as borrowing or stealing money, selling personal belongings, or frequently asking for financial assistance?")]
        [Required]

        public int Question3 { get; set; }
        [Display(Name = "Are your relationships with family and friends becoming strained, marked by arguments, decreased communication, or loss of trust ? ")]
        [Required]

        public int Question4 { get; set; }
        [Display(Name = "Are you associating yourself with a new group of friends who have a reputation for substance abuse?")]
        [Required]

        public int Question5 { get; set; }
        [Display(Name = "Do you have drug-related items, such as syringes, burnt spoons, drug wrappers, or drug paraphernalia hidden in personal belongings or living spaces")]
        [Required]

        public int Question6 { get; set; }
        [Display(Name = "Do you notice deteriorating physical health, such as frequent illnesses, unexplained injuries, or changes in appetite?")]
        [Required]

        public int Question7 { get; set; }
        [Required]

        [Display(Name = "Do you experience withdrawal symptoms when they attempt to stop using a substance, such as nausea, sweating, anxiety, insomnia, or irritability")]
        public int Question8 { get; set; }
        [Required]

        [Display(Name = "Is there a decline in fulfilling obligations at work, school, or home, frequent absenteeism, neglecting household chores, or missing deadlines")]
        public int Question9 { get; set; }
    }
}
