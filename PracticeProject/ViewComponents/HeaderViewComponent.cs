using Microsoft.AspNetCore.Mvc;

namespace PracticeProject.ViewComponents
{
    public class HeaderViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
