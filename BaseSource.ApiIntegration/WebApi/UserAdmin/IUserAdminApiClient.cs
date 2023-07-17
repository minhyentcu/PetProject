using BaseSource.ViewModels.Common;
using BaseSource.ViewModels.UserAdmin;

namespace BaseSource.ApiIntegration.WebApi.UserAdmin
{
    public interface IUserAdminApiClient
    {
        Task<ApiResult<PagedResult<UserAdminInfoDto>>> GetUserByFilter(UserAdminRequestDto model);
        Task<ApiResult<List<UserAdminInfoDto>>> GetAllManager();
        Task<ApiResult<string>> UpdateRoleManager(string userName);
        Task<ApiResult<string>> ResetPassword(ResetPasswordAdminDto model);
        Task<ApiResult<string>> Delete(string userId);
        Task<ApiResult<string>> Create(UserCreateAdminDto model);
        Task<ApiResult<string>> DeleteUserManagerBot(int id);
    }
}
