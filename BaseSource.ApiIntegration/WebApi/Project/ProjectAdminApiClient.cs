using BaseSource.ViewModels.Common;
using BaseSource.ViewModels.Project;
using EFCore.UnitOfWork.PageList;
using BaseSource.Shared.Extensions;
using BaseSource.Shared.Constants;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace BaseSource.ApiIntegration.WebApi.Project
{
    public class ProjectAdminApiClient : IProjectAdminApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProjectAdminApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ApiResult<string>> CreateAsync(PetProjectCreateDto model)
        {
            var client = _httpClientFactory.CreateClient(SystemConstants.BackendApiClient);
            var multiContent = new MultipartFormDataContent();
            if (model.ImageFile != null)
            {
                var fileStreamContent = new StreamContent(model.ImageFile.OpenReadStream());
                fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue(model.ImageFile.ContentType);
                multiContent.Add(fileStreamContent, "ImageFile", model.ImageFile.FileName);
            }
            multiContent.Add(new StringContent(model.Name), "Name");
            multiContent.Add(new StringContent(model.Description.ToString()), "Description");
            multiContent.Add(new StringContent(model.LinkDemo.ToString()), "LinkDemo");
            multiContent.Add(new StringContent(model.LinkSourceCode.ToString()), "LinkSourceCode");
            multiContent.Add(new StringContent(model.Image ?? string.Empty), "Image");
            multiContent.Add(new StringContent(model.CategoryProjectId.ToString()), "CategoryProjectId");
            
            using (var response = await client.PostAsync("api/admin/project", multiContent))
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ApiResult<string>>(responseString);
                return result;
            }
        }

        public async Task<ApiResult<string>> DeleteAsync(int id)
        {
            var client = _httpClientFactory.CreateClient(SystemConstants.BackendApiClient);
            return await client.DeleteAsync<ApiResult<string>>($"/api/admin/project/{id}");
        }

        public async Task<ApiResult<PagedResult<PetProjectDto>>> GetAllByFilterAsync(PetProjectRequestDto model)
        {
            var client = _httpClientFactory.CreateClient(SystemConstants.BackendApiClient);
            return await client.GetAsync<ApiResult<PagedResult<PetProjectDto>>>("/api/admin/projects",model);
        }

        public async Task<ApiResult<PetProjectDto>> GetByIdAsync(int id)
        {
            var client = _httpClientFactory.CreateClient(SystemConstants.BackendApiClient);
            return await client.GetAsync<ApiResult<PetProjectDto>>($"/api/admin/project/{id}");
        }

        public async Task<ApiResult<string>> PublishedAsync(int id)
        {
            var obj = new { };
            var client = _httpClientFactory.CreateClient(SystemConstants.BackendApiClient);
            return await client.PutAsync<ApiResult<string>>($"/api/admin/project/{id}/published", obj);
        }

        public async Task<ApiResult<string>> UpdateAsync(int id, PetProjectUpdateDto model)
        {
            var client = _httpClientFactory.CreateClient(SystemConstants.BackendApiClient);
            var multiContent = new MultipartFormDataContent();
            if (model.ImageFile != null)
            {
                var fileStreamContent = new StreamContent(model.ImageFile.OpenReadStream());
                fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue(model.ImageFile.ContentType);
                multiContent.Add(fileStreamContent, "ImageFile", model.ImageFile.FileName);
            }
            multiContent.Add(new StringContent(model.Name), "Name");
            multiContent.Add(new StringContent(model.Description.ToString()), "Description");
            multiContent.Add(new StringContent(model.LinkDemo.ToString()), "LinkDemo");
            multiContent.Add(new StringContent(model.LinkSourceCode.ToString()), "LinkSourceCode");
            multiContent.Add(new StringContent(model.Image ?? string.Empty), "Image");
            multiContent.Add(new StringContent(model.CategoryProjectId.ToString()), "CategoryProjectId");

            using (var response = await client.PutAsync($"api/admin/project/{id}", multiContent))
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ApiResult<string>>(responseString);
                return result;
            }
        }
    }
}
