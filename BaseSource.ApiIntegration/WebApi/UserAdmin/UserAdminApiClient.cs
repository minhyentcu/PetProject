using BaseSource.ViewModels.UserAdmin;
using BaseSource.Shared.Extensions;
using BaseSource.Shared.Constants;
using BaseSource.ViewModels.Common;

namespace BaseSource.ApiIntegration.WebApi.UserAdmin
{
    public class UserAdminApiClient : IUserAdminApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserAdminApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ApiResult<string>> Create(UserCreateAdminDto model)
        {
            var client = _httpClientFactory.CreateClient(SystemConstants.BackendApiClient);
            return await client.PostAsync<ApiResult<string>>($"/api/admin/user/create", model);
        }

        public async Task<ApiResult<string>> Delete(string userId)
        {
            var client = _httpClientFactory.CreateClient(SystemConstants.BackendApiClient);
            return await client.PostAsync<ApiResult<string>>($"/api/admin/user/delete?userId={userId}");
        }

        public async Task<ApiResult<string>> DeleteUserManagerBot(int id)
        {
            var client = _httpClientFactory.CreateClient(SystemConstants.BackendApiClient);
            return await client.PostAsync<ApiResult<string>>($"/api/admin/user/managerBot/delete/{id}");
        }

        public async Task<ApiResult<List<UserAdminInfoDto>>> GetAllManager()
        {
            var client = _httpClientFactory.CreateClient(SystemConstants.BackendApiClient);
            return await client.GetAsync<ApiResult<List<UserAdminInfoDto>>>($"/api/admin/user/managers");
        }

        

        public async Task<ApiResult<PagedResult<UserAdminInfoDto>>> GetUserByFilter(UserAdminRequestDto model)
        {
            var client = _httpClientFactory.CreateClient(SystemConstants.BackendApiClient);
            return await client.GetAsync<ApiResult<PagedResult<UserAdminInfoDto>>>($"/api/admin/users", model);
        }
        public async Task<ApiResult<string>> ResetPassword(ResetPasswordAdminDto model)
        {
            var client = _httpClientFactory.CreateClient(SystemConstants.BackendApiClient);
            return await client.PostAsync<ApiResult<string>>($"/api/admin/user/reset-password", model);
        }

        public async Task<ApiResult<string>> UpdateRoleManager(string userName)
        {
            var client = _httpClientFactory.CreateClient(SystemConstants.BackendApiClient);
            return await client.PostAsync<ApiResult<string>>($"/api/admin/user/manager/role?userName={userName}");
        }
    }
}
