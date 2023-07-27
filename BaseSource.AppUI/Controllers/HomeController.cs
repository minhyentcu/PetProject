using BaseSource.ApiIntegration.WebApi.ProjectClient;
using BaseSource.ApiIntegration.WebApi.Setting;
using BaseSource.ViewModels.Project;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseSource.AppUI.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IProjectApiClient _projectApiClient;
        private readonly IConfigSettingApiClient _configSettingApiClient;

        public HomeController(IProjectApiClient projectApiClient
            , IConfigSettingApiClient configSettingApiClient
            )
        {
            _projectApiClient = projectApiClient;
            _configSettingApiClient = configSettingApiClient;
        }
        [Route("/")]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var projects = await _projectApiClient.GetAllByFilterAsync(new ProjectClientRequestDto
            {
                Page = 1,
                PageSize = 9
            });
            return View(projects.ResultObj.Items);
        }
        [Route("/project/{slug}")]
        [AllowAnonymous]
        public async Task<IActionResult> ProjectInfo(string slug)
        {
            var projectInfo = await _projectApiClient.GetByIdAsync(slug,IpConnection);
            if (projectInfo == null || projectInfo.ResultObj == null)
            {
                return NotFound();
            }
            return View(projectInfo.ResultObj);
        }
        [Route("/lien-he")]
        [AllowAnonymous]
        public async Task<IActionResult> About()
        {
            var setting = await _configSettingApiClient.GetSetting();
            if (setting == null)
            {
                return NotFound();
            }
            return View(setting.ResultObj);
        }

        [Route("/getproject")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProject(int page = 2)
        {
            var projects = await _projectApiClient.GetAllByFilterAsync(new ProjectClientRequestDto
            {
                Page = page,
                PageSize = 9
            });
            return PartialView("_ProjectPaging", projects.ResultObj.Items);
        }
        [Route("/bang-xep-hang")]
        [AllowAnonymous]
        public async Task<IActionResult> ProjectRanking()
        {
            var projects = await _projectApiClient.GetAllByFilterAsync(new ProjectClientRequestDto
            {
                Page = 1,
                PageSize = 9
            });
            return View(projects.ResultObj.Items);
        }

        [Route("/project/voting")]
        [AllowAnonymous]
        public async Task<IActionResult> Voting(VoteProjectUpdateDto model)
        {
            model.IP = IpConnection;
            var result = await _projectApiClient.VotingAsync(model);
            if (result == null || !result.IsSuccessed)
            {
                return Json(new { isSuccess = false, message = result?.Message ?? "Lỗi không cập nhật!" });
            }
            return Json(new { isSuccess = true });
        }
    }
}
