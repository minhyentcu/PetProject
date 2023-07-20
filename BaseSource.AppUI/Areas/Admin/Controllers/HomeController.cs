using BaseSource.ApiIntegration.WebApi.ProjectClient;
using BaseSource.ViewModels.Project;
using Microsoft.AspNetCore.Mvc;

namespace BaseSource.AppUI.Areas.Admin.Controllers
{
    public class HomeController : BaseAdminController
    {
        private readonly IProjectApiClient _projectApiClient;
        public HomeController(IProjectApiClient projectApiClient)
        {
            _projectApiClient = projectApiClient;
        }
        [HttpGet]
        [Route("/admin")]
        public async Task<IActionResult> Index()
        {
           
            return Redirect("/admin/user/index");
            //return View();
        }
    }
}
