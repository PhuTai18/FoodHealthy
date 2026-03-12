using Microsoft.AspNetCore.Mvc;

namespace FoodHealthy.ViewComponents
{
    public class AdminHeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Views/Admin/Shared/_HeaderAdmin.cshtml");
        }
    }
}