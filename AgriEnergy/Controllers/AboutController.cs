using Microsoft.AspNetCore.Mvc;

namespace AgriEnergy.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
