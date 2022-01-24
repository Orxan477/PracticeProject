using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PracticeProject.DAL;
using PracticeProject.Models;
using PracticeProject.Utilities;
using PracticeProject.ViewModels.Account;
using System;
using System.Threading.Tasks;

namespace PracticeProject.Controllers
{
    public class AccountController : Controller
    {
        private AppDbContext _context { get; set; }
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
        private IOptions<MailSettings> _mail;
        public AccountController(AppDbContext context,
                                 UserManager<ApplicationUser> usermanager,
                                 SignInManager<ApplicationUser> signInManager,
                                 IOptions<MailSettings> mail,
                                 RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = usermanager;
            _signInManager = signInManager;
            _mail = mail;
            _roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid) return View(register);
            ApplicationUser newUser = new ApplicationUser
            {
                FullName=register.FullName,
                UserName=register.UserName,
                Email=register.Email
            };
            IdentityResult identityResult =await  _userManager.CreateAsync(newUser, register.Password);
            if (identityResult.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                string link = Url.Action(nameof(VerifyEmail), "Account", new { userId = newUser.Id, token },
                                                                            Request.Scheme, Request.Host.ToString());
                Email.SendEmailAsync(_mail.Value.Mail, _mail.Value.Password, newUser.Email,
                                                  $"<a href=\"{link}\">Verify Email</a>", "Confirmation Link Project");
                ViewBag.IsSuccessRegister = true;
                await _userManager.AddToRoleAsync(newUser, UserRoles.Admin.ToString());
                return View();
            }
            else
            {
                foreach (var item in identityResult.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }
        }
        public async Task<IActionResult> VerifyEmail(string userid,string token)
        {
            var user = await _userManager.FindByIdAsync(userid);
            if (user == null) return BadRequest();
            var result =await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                Email.SendEmailAsync(_mail.Value.Mail, _mail.Value.Password, _mail.Value.Mail,
                                                                         "Email Confirmed", "Verify Email");
                user.IsActive = true;
                ViewBag.IsSuccesVerify = true;
                
                await _context.SaveChangesAsync();
                return View(nameof(Login));
            }
            else return BadRequest();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM login,string ReturnUrl)
        {
            if (!ModelState.IsValid) return View(login);
            var user = await _userManager.FindByEmailAsync(login.Email.ToString());
            if(user is null)
            {
                ModelState.AddModelError(string.Empty, "Email and Password no correct");
                return View(login);
            }
            if (!user.IsActive)
            {
                ModelState.AddModelError(string.Empty, "Please confirm Email");
                return View(login);
            }
            var result = await _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, true);
            if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "Your account is locked. Please wait.");
                return View(login);
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Email and Password no correct");
                return View(login);
            }
            var device = Environment.MachineName.ToString();
            var osVersion = Environment.OSVersion.ToString();
            Email.SendEmailAsync(_mail.Value.Mail, _mail.Value.Password, user.Email, $"Dear {user.FullName} Your account has been logged in from this {device} device. Version={osVersion}. " +
                                                            "If you are not, change your password.", "Login Project");
            if (ReturnUrl is null)
            {
                return LocalRedirect(ReturnUrl);
        }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> Logout()
        {
            var user = _userManager.GetUserAsync(User);
            if (user is null) return NotFound();

          await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
     public async Task CreateRole()
        {
            foreach (var role in Enum.GetValues(typeof(UserRoles)))
            {
                if(!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                   await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
                }
            }
        }
    }
}
