using BaseSource.ViewModels.Common;
using BaseSource.ViewModels.Project;
using EFCore.UnitOfWork.PageList;

namespace BaseSource.ApiIntegration.WebApi.Project
{
    public interface IProjectAdminApiClient
    {
        Task<ApiResult<string>> CreateAsync(PetProjectCreateDto model);
        Task<ApiResult<string>> UpdateAsync(int id, PetProjectUpdateDto model);
        Task<ApiResult<string>> PublishedAsync(int id);
        Task<ApiResult<string>> DeleteAsync(int id);
        Task<ApiResult<PagedResult<PetProjectDto>>> GetAllByFilterAsync(PetProjectRequestDto model);
        Task<ApiResult<PetProjectDto>> GetByIdAsync(int id);
    }
}
