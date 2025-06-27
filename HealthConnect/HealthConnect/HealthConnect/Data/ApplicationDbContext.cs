using HealthConnect.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HealthConnect.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<UserDetails> UserDetail { get; set; }
        public DbSet<SelfAssessment> SelfAssessments { get; set; }
        public DbSet<Counselors> Counselor { get; set; }
        public DbSet<CounselorType> CounselorTypes { get; set; }
        public DbSet<CounselorAppointment> CounselorAppointments { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<DoctorCounselor> DoctorCounselors { get; set; }
        public DbSet<SessionType> SessionTypes { get; set; }
        public DbSet<ReferralLetter> ReferralLetters { get; set; }
    }
}
