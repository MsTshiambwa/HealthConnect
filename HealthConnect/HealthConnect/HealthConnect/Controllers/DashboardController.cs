using HealthConnect.Models;
using HealthConnect.Data.Classes;
using HealthConnect.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace HealthConnect.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<UserDetails> _userManager;

        public DashboardController(ApplicationDbContext context, UserManager<UserDetails> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Settings()
        {

            var user = _context.UserDetail.Where(x => x.Id == HttpContext.Session.GetString("user_id")).FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }
            
            return View(user);
        }
        public IActionResult Appointments()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CancelApplication(int? ID)
        {
            if (ID == 0 || ID == null)
            {
                TempData["Error"] = "";

                return RedirectToAction(nameof(Index));
            }
            var app = await _context.Appointments.FindAsync(ID);
            if (app == null)
            {
                TempData["Error"] = "Sorry. Appointment was not found.";
                return RedirectToAction(nameof(Index));
            }
            app.Status = "Canceled";
            _context.Appointments.Update(app);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ManageAppointments));
        }
        public async Task<IActionResult> EditApplication(int? ID)
        {
            if (ID == 0 || ID == null)
            {
                TempData["Error"] = "Sorry. Appointment was not found.";
                return RedirectToAction(nameof(Index));
            }
            var app = await _context.Appointments.FindAsync(ID);
            if (app == null)
            {
                TempData["Error"] = "Sorry. Appointment was not found.";
                return RedirectToAction(nameof(Index));
            }
             if (app.Department == Department.Counselor)
            {
                return RedirectToAction("EditApplication", "Counselor", new { ID = app.AppointmentID });

            }
            //error message
            return View(nameof(Index));
        }

        public IActionResult ManageAppointments()
        {
            var m = _context.Appointments.Where(x => x.PatientID == HttpContext.Session.GetString("user_id")).ToList();
            return View(m);
        }
        public IActionResult MedicalRecord()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateEmail(string? Email, string? ConfirmEmail)
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(ConfirmEmail))
            {
                TempData["ErrorE"] = "Please fill in everything";
                return RedirectToAction(nameof(Settings), new
                {
                    ID = HttpContext.Session.GetString("user_id")
                });
            }
            var obj = await _context.UserDetail.Where(x => x.Id == HttpContext.Session.GetString("user_id")).FirstOrDefaultAsync();
            if (obj == null)
            {
                TempData["ErrorE"] = "Something went wrong.Please try again";
                return RedirectToAction(nameof(Settings), new
                {
                    ID = HttpContext.Session.GetString("user_id")
                });
            }
            string token = await _userManager.GenerateChangeEmailTokenAsync(obj, Email);
            var result = await _userManager.ChangeEmailAsync(obj, ConfirmEmail, token);
            if (result.Succeeded)
            {
                TempData["pass"] = "Successfully updated Email Address.";
                return RedirectToAction(nameof(Settings), new { ID = HttpContext.Session.GetString("user_id") });

            }
            TempData["ErrorE"] = "Something went wrong.Please try again";
            return RedirectToAction(nameof(Settings), new { ID = HttpContext.Session.GetString("user_id") });
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePhoneNumber(string? PhoneNumber, string? ConfirmPhoneNmuber)
        {
            if (string.IsNullOrEmpty(PhoneNumber) || string.IsNullOrEmpty(ConfirmPhoneNmuber))
            {
                TempData["ErrorPN"] = "Please fill in everything";
                return RedirectToAction(nameof(Settings), new
                {
                    ID = HttpContext.Session.GetString("user_id")
                });
            }
            var obj = await _context.UserDetail.Where(x => x.Id == HttpContext.Session.GetString("user_id")).FirstOrDefaultAsync();
            if (obj == null)
            {
                TempData["ErrorPN"] = "Something went wrong.Please try again";
                return RedirectToAction(nameof(Settings), new
                {
                    ID = HttpContext.Session.GetString("user_id")
                });
            }
            string token = await _userManager.GenerateChangePhoneNumberTokenAsync(obj, PhoneNumber);
            var result = await _userManager.ChangePhoneNumberAsync(obj, ConfirmPhoneNmuber, token);
            if (result.Succeeded)
            {
                TempData["pass"] = "Successfully updated Phone Number.";
                return RedirectToAction(nameof(Settings), new { ID = HttpContext.Session.GetString("user_id") });

            }
            TempData["ErrorPN"] = "Something went wrong.Please try again";
            return RedirectToAction(nameof(Settings), new { ID = HttpContext.Session.GetString("user_id") });
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePassword(string? OldPassword, string? NewPassowrd, string? ConfirmNewPassowrd)
        {
            if (string.IsNullOrEmpty(OldPassword) || string.IsNullOrEmpty(NewPassowrd) || string.IsNullOrEmpty(ConfirmNewPassowrd))
            {
                TempData["ErrorP"] = "Please fill in everything";
                return RedirectToAction(nameof(Settings), new
                {
                    ID = HttpContext.Session.GetString("user_id")
                });

            }
            if (NewPassowrd != ConfirmNewPassowrd)
            {
                TempData["ErrorP"] = "New password does not match.";
            }
            var obj = await _context.UserDetail.Where(x => x.Id == HttpContext.Session.GetString("user_id")).FirstOrDefaultAsync();
            if (obj == null)
            {
                TempData["ErrorP"] = "Something went wrong.Please try again";
                return RedirectToAction(nameof(Settings), new
                {
                    ID = HttpContext.Session.GetString("user_id")
                });
            }
            var result = await _userManager.ChangePasswordAsync(obj, OldPassword, NewPassowrd);
            if (result.Succeeded)
            {
                TempData["pass"] = "Successfully updated Password.";
                return RedirectToAction(nameof(Settings), new
                {
                    ID = HttpContext.Session.GetString("user_id")
                });
            }
            TempData["ErrorP"] = "Something went wrong.Please try again";
            return RedirectToAction(nameof(Settings), new
            {
                ID = HttpContext.Session.GetString("user_id")
            });

        }
    }
}