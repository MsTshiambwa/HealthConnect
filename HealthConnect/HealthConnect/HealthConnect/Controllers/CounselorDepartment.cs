using HealthConnect.Data.Classes;
using HealthConnect.Data;
using HealthConnect.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace HealthConnect.Controllers
{
    [Authorize]
    public class CounselorDepartment : Controller
    {
        private readonly ApplicationDbContext _context;
        public CounselorDepartment(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> ViewAppointments()
        {
            var app = _context.CounselorAppointments.Where(x => x.Status == "Approved").ToList();
            var patient = _context.UserDetail;
            var model = from a in app
                        join p in patient on a.PatientID equals p.Id
                        select new AppointmentViewModel
                        {
                            PatientName = $"{p.Name} {p.Surname}",
                            Reason = a.Reason,
                            Status = a.Status,
                            Date = a.Date,
                            IDNumber = p.IDNumber
                        };
            return View(model);
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddSession()
        {
            return View();
        }
        public IActionResult AddReferral()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddReferral(ReferralLetter referral)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Something is missing in the form.";
                return View(referral);
            }
            var pat = await _context.UserDetail.Where(x => x.IDNumber == referral.PatientIDNumber).FirstOrDefaultAsync();
            if (pat == null)
            {
                TempData["error"] = "Invalid Id number.";
                return View(referral);
            }
            await _context.ReferralLetters.AddAsync(referral);
            await _context.SaveChangesAsync();
            TempData["result"] = $"Referral Letter has been added for {pat.Name} {pat.Surname}";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult ListReferral()
        {
            var model = _context.ReferralLetters.Where(x => x.DoctorIDNumber == HttpContext.Session.GetString("user_idnumber")).ToList();
            return View(model);
        }
        [HttpPost]
        public IActionResult SearchPatientLetter(string? ID)
        {
            if (string.IsNullOrEmpty(ID))
            {
                TempData["error"] = "Invalid Id Number.";
                return RedirectToAction(nameof(ListReferral));
            }
            var model = _context.ReferralLetters.Where(x => x.PatientIDNumber == ID).ToList();
            return View(nameof(ListReferral), model);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateReferral(ReferralLetter referral)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Something is missing in the form.";
                return View(referral);
            }
            _context.ReferralLetters.Update(referral);
            await _context.SaveChangesAsync();
            TempData["result"] = $"Referral Letter has been updated.";
            return RedirectToAction(nameof(ListReferral));
        }
        public IActionResult ListSession()
        {
            var model = _context.SessionTypes.Where(x => x.DoctorID == HttpContext.Session.GetString("user_idnumber")).ToList();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateSessionID(int? ID)
        {
            if (ID == null || ID <= 0)
            {
                TempData["error"] = "Session not found.";
                return RedirectToAction(nameof(ListSession));
            }

            var model = await _context.SessionTypes.Where(x => x.CounsellingSessionId == ID).FirstOrDefaultAsync();
            if (model == null)
            {
                TempData["error"] = "Session not found.";
                return RedirectToAction(nameof(ListSession));
            }
            return View(nameof(UpdateSession), model);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateReferralID(int? ID)
        {
            if (ID == null || ID <= 0)
            {
                TempData["error"] = "Referral letter not found.";
                return RedirectToAction(nameof(ListSession));
            }

            var model = await _context.ReferralLetters.Where(x => x.Id == ID).FirstOrDefaultAsync();
            if (model == null)
            {
                TempData["error"] = "Referral letter not found.";
                return RedirectToAction(nameof(ListSession));
            }
            return View(nameof(UpdateReferral), model);
        }
        [HttpPost]
        public IActionResult SearchPatientSession(string? ID)
        {
            if (string.IsNullOrEmpty(ID))
            {
                TempData["error"] = "Invalid Patient ID";
                return RedirectToAction(nameof(ListSession));
            }

            var model = _context.SessionTypes.Where(x => x.DoctorID == HttpContext.Session.GetString("user_idnumber") && x.PatientID == ID).ToList();
            if (model == null)
            {
                TempData["error"] = "Invalid Patient ID";
                return RedirectToAction(nameof(ListSession));
            }
            return View(nameof(ListSession), model);
        }
        [HttpPost]
        public async Task<IActionResult> AddSession(SessionType session)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Something is missing in the form.";
                return View(session);
            }
            var pat = await _context.UserDetail.Where(x => x.IDNumber == session.PatientID).FirstOrDefaultAsync();
            var doc = await _context.UserDetail.Where(x => x.IDNumber == session.DoctorID).FirstOrDefaultAsync();
            if (pat == null)
            {
                TempData["error"] = "Patient ID Number is invalid.";
                return View(session);
            }
            session.PatientName = $"{pat.Name} {pat.Surname}";
            session.DoctorName = $"{doc.Name} {doc.Surname}";
            await _context.SessionTypes.AddAsync(session);
            await _context.SaveChangesAsync();
            TempData["result"] = $"Session has been added for {pat.Name} {pat.Surname}";
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> UpdateSession(SessionType session)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Something is missing in the form.";
                return View(session);
            }
            var pat = await _context.UserDetail.Where(x => x.IDNumber == session.PatientID).FirstOrDefaultAsync();
            var doc = await _context.UserDetail.Where(x => x.IDNumber == session.DoctorID).FirstOrDefaultAsync();
            if (pat == null)
            {
                TempData["error"] = "Something went wrong please try again.";
                return View(session);
            }
            session.PatientName = $"{pat.Name} {pat.Surname}";
            session.DoctorName = $"{doc.Name} {doc.Surname}";
            _context.SessionTypes.Update(session);
            await _context.SaveChangesAsync();
            TempData["result"] = $"Session has been Updated for {pat.Name} {pat.Surname}";
            return RedirectToAction(nameof(ListSession));
        }
    }
}