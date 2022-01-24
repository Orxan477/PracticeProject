using Microsoft.AspNetCore.Mvc;

namespace PracticeProject.ViewComponents
{
    public class FooterViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
