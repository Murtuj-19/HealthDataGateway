using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using HealthDataGateway.Data;
using Microsoft.EntityFrameworkCore;
using HealthDataGateway.Data.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

namespace HealthDataGateway.Web.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher<HealthDataGateway.Models.User> _passwordHasher;

        public LoginModel(ApplicationDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<HealthDataGateway.Models.User>();
        }

        [BindProperty]
        [Required]
        public string Username { get; set; }

        [BindProperty]
        [Required]
        public string Password { get; set; }

        [BindProperty]
        public string RequestedRole { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            bool isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";

            if (!ModelState.IsValid)
            {
                if (isAjax)
                    return new JsonResult(new { success = false, message = "Please provide username and password." });

                return Page();
            }

            var user = await _context.Set<HealthDataGateway.Models.User>()
                .FirstOrDefaultAsync(u => u.Username == Username && u.IsActive);

            if (user == null)
            {
                if (isAjax)
                    return new JsonResult(new { success = false, message = "Invalid credentials." });

                ModelState.AddModelError(string.Empty, "Invalid credentials");
                return Page();
            }

            // Verify password: support hashed passwords and legacy plain-text
            var passwordVerification = PasswordVerificationResult.Failed;
            if (!string.IsNullOrEmpty(user.PasswordHash))
            {
                try
                {
                    passwordVerification = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, Password);
                }
                catch
                {
                    passwordVerification = PasswordVerificationResult.Failed;
                }
            }

            bool passwordValid = passwordVerification == PasswordVerificationResult.Success || user.PasswordHash == Password;

            if (!passwordValid)
            {
                if (isAjax)
                    return new JsonResult(new { success = false, message = "Invalid credentials." });

                ModelState.AddModelError(string.Empty, "Invalid credentials");
                return Page();
            }

            // If RequestedRole provided, ensure the user has that role
            if (!string.IsNullOrEmpty(RequestedRole) && !string.Equals(user.UserType, RequestedRole, System.StringComparison.OrdinalIgnoreCase))
            {
                if (isAjax)
                    return new JsonResult(new { success = false, message = "User does not have the required role for this module." });

                ModelState.AddModelError(string.Empty, "User does not have the required role for this module.");
                return Page();
            }

            var claims = new[] {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("UserId", user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user.UserType)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            // Update last login
            user.LastLogin = System.DateTime.Now;
            await _context.SaveChangesAsync();

            // Determine redirect
            string redirectUrl = "/";
            if (user.UserType == "CONNECTOR")
                redirectUrl = Url.Page("/Connector/Index");
            else if (user.UserType == "SOURCE")
                redirectUrl = Url.Page("/SourceHospital/Index");
            else if (user.UserType == "TARGET")
                redirectUrl = Url.Page("/TargetHospital/Index");

            if (isAjax)
                return new JsonResult(new { success = true, redirectUrl });

            return Redirect(redirectUrl);
        }
    }
}
