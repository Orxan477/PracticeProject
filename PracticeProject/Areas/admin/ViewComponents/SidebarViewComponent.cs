using Microsoft.AspNetCore.Mvc;

namespace PracticeProject.Areas.admin.ViewComponents
{
    public class SidebarViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
