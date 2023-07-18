using BaseSource.ViewModels.Common;
using BaseSource.ViewModels.Project;
using EFCore.UnitOfWork.PageList;

namespace BaseSource.ApiIntegration.WebApi.Category
{
    public interface ICategoryService
    {
        Task<ApiResult<string>> CreateAsync(CategoryCreateDto model);
        Task<ApiResult<string>> UpdateAsync(int id, CategoryUpdateDto model);
        Task<ApiResult<string>> PublishedAsync(int id);
        Task<ApiResult<string>> DeleteAsync(int id);
        Task<ApiResult<PagedResult<CategoryInfoDto>>> GetAllByFilterAsync(CategoryRequestDto model);
        Task<ApiResult<CategoryInfoDto>> GetByIdAsync(int id);
    }
}
