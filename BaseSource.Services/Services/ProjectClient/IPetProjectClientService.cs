using BaseSource.ViewModels.Project;
using EFCore.UnitOfWork.PageList;

namespace BaseSource.Services.Services.ProjectClient
{
    public interface IPetProjectClientService
    {
        Task<IPagedList<PetProjectDto>> GetAllByFilterAsync(ProjectClientRequestDto model);
        Task<PetProjectDto> GetByIdAsync(string slug);
    }
}
