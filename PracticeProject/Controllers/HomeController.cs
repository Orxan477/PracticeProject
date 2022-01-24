using Microsoft.AspNetCore.Mvc;
using PracticeProject.DAL;
using System.Linq;

namespace PracticeProject.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Cards.Where(x=>!x.IsDeleted).ToList());
        }
    }
}
