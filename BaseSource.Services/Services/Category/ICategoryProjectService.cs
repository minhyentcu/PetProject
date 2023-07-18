using BaseSource.ViewModels.Project;
using EFCore.UnitOfWork.PageList;

namespace BaseSource.Services.Services.Category
{
    public interface ICategoryProjectService
    {
        Task<KeyValuePair<bool, string>> CreateAsync(CategoryCreateDto model);
        Task<KeyValuePair<bool, string>> UpdateAsync(int id, CategoryUpdateDto model);
        Task<KeyValuePair<bool, string>> PublishedAsync(int id);
        Task<KeyValuePair<bool, string>> DeleteAsync(int id);
        Task<IPagedList<CategoryInfoDto>> GetAllByFilterAsync(CategoryRequestDto model);
        Task<CategoryInfoDto> GetByIdAsync(int id);
    }
}
