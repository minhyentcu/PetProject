using BaseSource.ApiIntegration.WebApi.Setting;
using Microsoft.AspNetCore.Mvc;

namespace BaseSource.AppUI.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly IConfigSettingApiClient _configSettingApiClient;
        public HeaderViewComponent(IConfigSettingApiClient configSettingApiClient)
        {
            _configSettingApiClient = configSettingApiClient;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var config = await _configSettingApiClient.GetSetting();
            return View(config.ResultObj);
        }
    }
}
