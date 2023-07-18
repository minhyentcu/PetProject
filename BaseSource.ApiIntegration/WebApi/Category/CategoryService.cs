using BaseSource.ViewModels.Common;
using BaseSource.ViewModels.Project;
using EFCore.UnitOfWork.PageList;
using BaseSource.Shared.Helpers;
using BaseSource.Shared.Constants;
using BaseSource.Shared.Extensions;

namespace BaseSource.ApiIntegration.WebApi.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CategoryService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ApiResult<string>> CreateAsync(CategoryCreateDto model)
        {
            var client = _httpClientFactory.CreateClient(SystemConstants.BackendApiClient);
            return await client.PostAsync<ApiResult<string>>($"/api/admin/category", model);
        }

        public async Task<ApiResult<string>> DeleteAsync(int id)
        {
            var client = _httpClientFactory.CreateClient(SystemConstants.BackendApiClient);
            return await client.DeleteAsync<ApiResult<string>>($"/api/admin/category/{id}");
        }

        public async Task<ApiResult<PagedResult<CategoryInfoDto>>> GetAllByFilterAsync(CategoryRequestDto model)
        {
            var client = _httpClientFactory.CreateClient(SystemConstants.BackendApiClient);
            return await client.GetAsync<ApiResult<PagedResult<CategoryInfoDto>>>("/api/admin/categorys");
        }

        public async Task<ApiResult<CategoryInfoDto>> GetByIdAsync(int id)
        {
            var client = _httpClientFactory.CreateClient(SystemConstants.BackendApiClient);
            return await client.GetAsync<ApiResult<CategoryInfoDto>>($"/api/admin/category/{id}");
        }

        public async Task<ApiResult<string>> PublishedAsync(int id)
        {
            var obj= new { };
            var client = _httpClientFactory.CreateClient(SystemConstants.BackendApiClient);
            return await client.PutAsync<ApiResult<string>>($"/api/admin/category/{id}/published", obj);
        }

        public async Task<ApiResult<string>> UpdateAsync(int id, CategoryUpdateDto model)
        {
            var client = _httpClientFactory.CreateClient(SystemConstants.BackendApiClient);
            return await client.PutAsync<ApiResult<string>>($"/api/admin/category/{id}", model);
        }
    }
}
