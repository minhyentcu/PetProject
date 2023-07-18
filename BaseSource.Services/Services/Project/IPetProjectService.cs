using BaseSource.ViewModels.Project;
using EFCore.UnitOfWork.PageList;

namespace BaseSource.Services.Services.Project
{
    public interface IPetProjectService
    {
        Task<KeyValuePair<bool, string>> CreateAsync(PetProjectCreateDto model);
        Task<KeyValuePair<bool, string>> UpdateAsync(int id, PetProjectUpdateDto model);
        Task<KeyValuePair<bool, string>> PublishedAsync(int id);
        Task<KeyValuePair<bool, string>> DeletedAsync(int id);
        Task<IPagedList<PetProjectDto>> GetByFilterAsync(PetProjectRequestDto model);
        Task<PetProjectDto> GetByIdAsync(int id);
    }
}
