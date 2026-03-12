using Microsoft.AspNetCore.Mvc;

namespace FoodHealthy.ViewComponents
{
    public class AdminSidebarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Views/Admin/Shared/_SidebarAdmin.cshtml");
        }
    }
}