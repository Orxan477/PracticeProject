using Microsoft.AspNetCore.Mvc;

namespace PracticeProject.Areas.admin.ViewComponents
{
    public class NavbarViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
