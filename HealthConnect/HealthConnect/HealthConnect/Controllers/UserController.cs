
using HealthConnect.Data;
using HealthConnect.Data.Classes;
using HealthConnect.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace HealthConnect.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<UserDetails> _userManager;
        private readonly SignInManager<UserDetails> _signInManager;
        private readonly IHttpContextAccessor _accessor;
        public UserController(UserManager<UserDetails> userManager, SignInManager<UserDetails> signInManager, IHttpContextAccessor accessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accessor = accessor;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetStarted(Login login)
        {
            if (login == null)
            {
                return View("Register");
            }
            var user = await _userManager.FindByEmailAsync(login.EmailAddress);
            if (user != null)
            {
                return View("Login", login);
            }
            else
            {
                Register register = new()
                {
                    EmailAddress = login.EmailAddress
                };
                return View("Register", register);
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var user = await _userManager.FindByEmailAsync(login.EmailAddress);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, login.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, login.Password, false, false);
                    if (result.Succeeded)
                    {
                        HttpContext.Session.SetString("user_id", user.Id);
                        HttpContext.Session.SetString("user_name", $"{user.Name} {user.Surname} ({user.Role})");
                        HttpContext.Session.SetString("user_idnumber", user.IDNumber);
                        if (!string.IsNullOrEmpty(user.Department))
                        {
                            HttpContext.Session.SetString("user_role", user.Department);
                        }
                        else
                        {
                            HttpContext.Session.SetString("user_role", user.Role);

                        }

                        TempData["Result"] = "Successfully Logged-in";
                        if (user.Role == Roles.Patient)
                        {
                            return RedirectToAction("Index", "Dashboard");
                        }
                        else if (user.Role == "Counselor")
                        {
                            if (user.Role == Department.Counselor)
                            {
                                HttpContext.Session.SetString("user_name", $"{user.Name} {user.Surname} (Counselor)");
                                return RedirectToAction("Index", nameof(CounselorDepartment));
                            }

                            TempData["Error"] = "Something is wrong with this account. Visit the clinic for more assistance";
                            return View(login);
                        }

                    }
                }
                TempData["Error"] = "Wrong credentials. Please, try again!";
                return View(login);
            }

            TempData["Error"] = "Wrong credentials. Please, try again!";
            return View(login);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Register register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }
            var user = await _userManager.FindByEmailAsync(register.EmailAddress);

            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(register);
            }

            var newUser = new UserDetails()
            {
                Name = register.Name,
                Surname = register.Surname,
                Email = register.EmailAddress,
                UserName = register.EmailAddress,
                EmailConfirmed = true,
                IDNumber = register.IDNumber,
                Password = register.Password,
                Gender = register.Gender,
                Address = register.Address,

                AreaCode = register.AreaCode,
                DateOfBirth = register.DateOfBirth,
                PhoneNumber = register.PhoneNumber,
                IsActive = register.IsActive,
                Role = register.Role,
                Department = register.Department,
                PhoneNumberConfirmed = true,
                LockoutEnabled = false
            };
            await _userManager.SetPhoneNumberAsync(newUser, newUser.PhoneNumber);
            await _userManager.SetEmailAsync(newUser, newUser.Email);
            await _userManager.SetUserNameAsync(newUser, newUser.Email);


            var newUserResponse = await _userManager.CreateAsync(newUser, register.Password);

            if (newUserResponse.Succeeded)
            {
                string token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                await _userManager.ConfirmEmailAsync(newUser, token);
                TempData["success"] = "Successfully Registered";
                return RedirectToAction("Login");

            }
            else
            {
                return View(register);
            }

        }



        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            HttpContext.Session.Remove("user_id");
            HttpContext.Session.Remove("user_name");
            HttpContext.Session.Remove("user_idnumber");

            return RedirectToAction("Index", "User");
        }

    }
}
