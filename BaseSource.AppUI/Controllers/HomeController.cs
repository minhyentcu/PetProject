using Microsoft.AspNetCore.Mvc;

namespace BaseSource.AppUI.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
