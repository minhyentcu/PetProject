using BaseSource.ViewModels.Common;
using BaseSource.ViewModels.Project;

namespace BaseSource.ApiIntegration.WebApi.ProjectClient
{
    public interface IProjectApiClient
    {
        Task<ApiResult<PagedResult<PetProjectDto>>> GetAllByFilterAsync(ProjectClientRequestDto model);
        Task<ApiResult<PetProjectDto>> GetByIdAsync(string slug);
        Task<ApiResult<string>> VotingAsync(VoteProjectUpdateDto model);
    }
}
