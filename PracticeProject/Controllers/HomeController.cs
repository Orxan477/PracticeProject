using Microsoft.AspNetCore.Mvc;

namespace PracticeProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
