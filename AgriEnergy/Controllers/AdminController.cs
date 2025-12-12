using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using AgriEnergy.Services;

namespace AgriEnergy.Controllers
{
    public class AdminController : Controller
    {
        private readonly EmailService _emailService;

        public AdminController(EmailService emailService)
        {
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            var userRole = HttpContext.Session.GetString("Role");
            if (userRole == null)
            {
                return RedirectToAction("Index", "Unauthorized");
            }
            else if (userRole.Equals("EMPLOYEE"))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Unauthorized");
            }
        }

        [HttpPost]
        public async Task<IActionResult> InviteFarmer(string email)
        {
            var link = Url.Action("Register", "Account", null, Request.Scheme);
            string subject = "AgriEnergy Farmer Invitation";
            string body = $"You have been invited to join AgriEnergy-Connect. Click the link to register: https://localhost:5017/Auth/Registration";

            await _emailService.SendEmailAsync(email, subject, body);
            TempData["Success"] = "Invitation sent!";
            return RedirectToAction("Index");
        }
    }
}