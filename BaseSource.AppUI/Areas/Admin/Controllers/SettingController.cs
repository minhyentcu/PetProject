using BaseSource.ApiIntegration.WebApi.Setting;
using BaseSource.ViewModels.Setting;
using Microsoft.AspNetCore.Mvc;

namespace BaseSource.AppUI.Areas.Admin.Controllers
{
    public class SettingController : BaseAdminController
    {
        private readonly IConfigSettingApiClient _configSettingApiClient;
        public SettingController(
            IConfigSettingApiClient configSettingApiClient
            )
        {
            _configSettingApiClient = configSettingApiClient;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _configSettingApiClient.GetSetting();
            if (result == null || result.ResultObj == null)
            {
                return NotFound();
            }
            return View(result.ResultObj);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ConfigSettingVm model)
        {
             await _configSettingApiClient.Update(model);
            return RedirectToAction("Index");
        }
    }
}
