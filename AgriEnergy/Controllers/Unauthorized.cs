using Microsoft.AspNetCore.Mvc;

namespace AgriEnergy.Controllers
{
    public class Unauthorized : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
