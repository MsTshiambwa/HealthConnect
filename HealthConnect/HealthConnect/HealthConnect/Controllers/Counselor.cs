using HealthConnect.Data;
using HealthConnect.Data.Classes;
using HealthConnect.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;


namespace HealthConnect.Controllers
{
    [Authorize]
    public class Counselor : Controller
    {
        private readonly ApplicationDbContext _context;
        public Counselor(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var x = await _context.CounselorAppointments.Where(x => x.Status == "Pending" || x.Status == "Approved" && x.PatientID == HttpContext.Session.GetString("user_id")).FirstOrDefaultAsync();
            if (x != null)
            {
                TempData["appointment"] = x.Date.ToShortDateString();
                TempData["ID"] = x.AppointmentID;
                TempData["reason"] = $"{x.Reason} ({x.Status})";
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Report(int? ReportID)
        {
            if (ReportID == null || ReportID < 0)
            {
                TempData["error"] = "Sorry. Report was not found.";
                return RedirectToAction(nameof(ListReport));
            }
            var model = await _context.SessionTypes.Where(x => x.CounsellingSessionId == ReportID && x.PatientID == HttpContext.Session.GetString("user_idnumber")).FirstOrDefaultAsync();
            if (model == null)
            {
                TempData["error"] = "Sorry. Report was not found.";
                return RedirectToAction(nameof(ListReport));
            }

            return View(nameof(Report), model);
        }
        public IActionResult Referral()
        {
            return View();
        }
        public IActionResult ListReport()
        {
            var model = _context.SessionTypes.Where(x => x.PatientID == HttpContext.Session.GetString("user_idnumber")).ToList();
            return View(model);
        }
        public IActionResult Results()
        {
            var x = _context.SelfAssessments.ToList();
            if (x != null)
            {
                return View(x);

            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateApplication(CounselorAppointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return View(appointment);
            }
            if (appointment.Date.Date < DateTime.Now.Date)
            {
                TempData["error"] = "Invalid Date.";
                return View();
            }
            _context.CounselorAppointments.Update(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction("ManageAppointment", "Dashboard");
        }
        public async Task<IActionResult> EditApplication(int? ID)
        {
            if (ID == 0 || ID == null)
            {
                TempData["error"] = "Sorry. Could not find Appointment";
                return RedirectToAction("ManageAppointment", "Dashboard");
            }
            var app = await _context.CounselorAppointments.FindAsync(ID);
            if (app == null)
            {
                TempData["error"] = "Sorry. Could not find Appointment";
                return RedirectToAction("ManageAppointment", "Dashboard");
            }
            var docs = _context.Counselor.Where(x => x.Role == Roles.Counselor);
            var counselor = _context.CounselorTypes;
            var list = from x in docs
                       join y in counselor
                       on x.CounselorType equals y.CounselorTypeId
                       select new
                       {
                           DoctorName = $"{x.Name} ({y.CounselorTypeName})",
                           DoctorID = x.Id
                       };
            ViewBag.Doc = new SelectList(list, "DoctorId", "DoctorName");
            return View("UpdateApplication", app);
        }
        public IActionResult SelfAssessment()
        {
            return View();
        }

        public IActionResult Awareness()
        {
            return View();
        }
        public IActionResult ListReferrals()
        {
            var model = _context.ReferralLetters.Where(x => x.PatientIDNumber == HttpContext.Session.GetString("user_idnumber")).ToList();
            var doc = _context.UserDetail;
            var x = from m in model
                    join d in doc on m.DoctorIDNumber equals d.IDNumber
                    select new ReferralLetterViewModel
                    {
                        CounsellingType = m.CounsellingType,
                        DoctorName = $"{d.Name} {d.Surname}",
                        DoctorPhoneNumber = d.PhoneNumber,
                        Reason = m.SpecialRemarks,
                        Date = m.Date,
                        Facility = m.FacilityName
                    };
            if (x != null)
            {
                return View(x);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Referral(int? ID)
        {
            if (ID == 0 || ID == null)
            {
                TempData["error"] = "Referral ID not found.";
                return RedirectToAction(nameof(ListReferrals));
            }
            var model = await _context.ReferralLetters.Where(x => x.PatientIDNumber == HttpContext.Session.GetString("user_idnumber") && x.Id == ID).FirstOrDefaultAsync();
            if (model == null)
            {
                TempData["error"] = "Referral ID not found.";
                return RedirectToAction(nameof(ListReferrals));
            }
            var doc = await _context.UserDetail.Where(x => x.IDNumber == model.DoctorIDNumber).FirstOrDefaultAsync();
            if (doc == null)
            {
                TempData["error"] = "Doctor was not found in the database.";
                return RedirectToAction(nameof(ListReferrals));
            }
            var pat = await _context.UserDetail.Where(x => x.IDNumber == HttpContext.Session.GetString("user_idnumber")).FirstOrDefaultAsync();

            ReferralLetterViewModel referral = new()
            {
                CounsellingType = model.CounsellingType,
                DoctorName = $"{doc.Name} {doc.Surname}",
                DoctorPhoneNumber = doc.PhoneNumber,
                Reason = model.SpecialRemarks,
                Date = model.Date,
                Facility = model.FacilityName
            };
            return View(referral);
        }
        [HttpPost]
        public IActionResult SelfAssessment(int? ID)
        {

            return View(nameof(Step1));
        }
        [HttpPost]
        public IActionResult Step1(Step1 step1)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Step1), step1);
            }
            double total = 0;
            total += step1.Question1;
            total += step1.Question2;
            total += step1.Question3;
            total += step1.Question4;
            total += step1.Question5;
            total += step1.Question6;
            total += step1.Question7;
            stepTotal[0] = total / 7;
            return View(nameof(Step2));
        }
        private static double[] stepTotal = new double[4];
        [HttpPost]
        public IActionResult Step2(Step2 step2)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Step2), step2);
            }
            double total = 0;
            total += step2.Question1;
            total += step2.Question2;
            total += step2.Question3;
            total += step2.Question4;
            total += step2.Question5;
            total += step2.Question6;
            total += step2.Question7;
            total += step2.Question8;
            total += step2.Question9;
            total += step2.Question10;
            total += step2.Question11;
            stepTotal[1] = total / 11;
            return View(nameof(Step3));
        }
        [HttpPost]
        public IActionResult Step3(Step3 step3)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Step3), step3);
            }
            double total = 0;
            total += step3.Question1;
            total += step3.Question2;
            total += step3.Question3;
            total += step3.Question4;
            total += step3.Question5;
            total += step3.Question6;
            total += step3.Question7;
            total += step3.Question8;
            total += step3.Question9;
            total += step3.Question10;
            stepTotal[2] = total / 10;
            return View(nameof(Step4));
        }
        [HttpPost]
        public async Task<IActionResult> Step4(Step4 step4)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Step4), step4);
            }
            int total = 0;
            total += step4.Question1;
            total += step4.Question2;
            total += step4.Question3;
            total += step4.Question4;
            total += step4.Question5;
            total += step4.Question6;
            total += step4.Question7;
            total += step4.Question8;
            total += step4.Question9;

            double x = total;
            stepTotal[3] = total / 9;

            string message = "You may need to visit a ";
            string col = "";
            double max = -1;
            for (int i = 0; i < stepTotal.Length - 1; i++)
            {
                max = Math.Max(stepTotal[i], max);
            }
            for (int i = 0; i < stepTotal.Length - 1; i++)
            {
                if (stepTotal[i] == max)
                {
                    if (i == 0)
                    {
                        message += "A GBV Counselor ";
                        col = "A GBV Counselor ";
                    }
                    if (i == 1)
                    {
                        message += "A Depression Counselor ";
                        col = "A Depression Counselor ";
                    }
                    if (i == 2)
                    {
                        message += "A Marriage Counselor ";
                        col = "A Marriage Counselor ";
                    }
                    if (i == 3)
                    {
                        message += "A Substance abuse Counselor ";
                        col = "A Substance abuse Counselor ";
                    }
                    SelfAssessment self = new()
                    {
                        Result = col,
                        Date = DateTime.Now
                    };
                    await _context.SelfAssessments.AddAsync(self);
                    await _context.SaveChangesAsync();
                    break;

                }
            }
            if (max == 0)
            {
                message = "Everything seems to be great. Keep up the happy smile.";
            }

            TempData["Result"] = message;
            return View("ResultAssessment");
            /*step 1 gbv
            step 2 depression
            step 3 marital issues
            step 4 substance abuse*/
        }
        public IActionResult Appointment()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Appointment(CounselorAppointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return View(appointment);
            }
            if (appointment.Date.Date < DateTime.Now.Date)
            {
                TempData["Error"] = "Invalid Date.";
                return View();
            }
            var obj = _context.CounselorAppointments.Where(x => x.Date.Date == appointment.Date.Date && x.PatientID == appointment.PatientID && x.Status != "Canceled").Count();
            if (obj > 0)
            {
                TempData["Error"] = "Booking Already Exists";
                return View();
            }
            await _context.CounselorAppointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
            TempData["Result"] = "Done";
            return View("Result");
        }
    }
}