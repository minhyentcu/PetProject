using BaseSource.ViewModels.Common;
using BaseSource.ViewModels.Project;
using EFCore.UnitOfWork.PageList;

namespace BaseSource.ApiIntegration.WebApi.ProjectClient
{
    public interface IProjectApiClient
    {
        Task<ApiResult<PagedResult<PetProjectDto>>> GetAllByFilterAsync(ProjectClientRequestDto model);
        Task<ApiResult<PetProjectDto>> GetByIdAsync(string slug);
    }
}
