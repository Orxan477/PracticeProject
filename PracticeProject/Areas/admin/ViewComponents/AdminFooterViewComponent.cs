using Microsoft.AspNetCore.Mvc;

namespace PracticeProject.Areas.admin.ViewComponents
{
    public class AdminFooterViewComponent:ViewComponent  
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
