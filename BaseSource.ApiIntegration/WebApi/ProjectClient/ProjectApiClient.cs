using BaseSource.Shared.Constants;
using BaseSource.ViewModels.Common;
using BaseSource.ViewModels.Project;
using EFCore.UnitOfWork.PageList;
using BaseSource.Shared.Extensions;

namespace BaseSource.ApiIntegration.WebApi.ProjectClient
{
    public class ProjectApiClient : IProjectApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProjectApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ApiResult<PagedResult<PetProjectDto>>> GetAllByFilterAsync(ProjectClientRequestDto model)
        {
            var client = _httpClientFactory.CreateClient(SystemConstants.BackendApiClient);
            return await client.GetAsync<ApiResult<PagedResult<PetProjectDto>>>("/api/projects", model);
        }

        public async Task<ApiResult<PetProjectDto>> GetByIdAsync(string slug, string ipConnect)
        {
            var client = _httpClientFactory.CreateClient(SystemConstants.BackendApiClient);
            return await client.GetAsync<ApiResult<PetProjectDto>>($"/api/project?slug={slug}&ip={ipConnect}");
        }

        public async Task<ApiResult<string>> VotingAsync(VoteProjectUpdateDto model)
        {
            var client = _httpClientFactory.CreateClient(SystemConstants.BackendApiClient);
            return await client.PatchAsync<ApiResult<string>>($"/api/project/voting", model);
        }
    }
}
