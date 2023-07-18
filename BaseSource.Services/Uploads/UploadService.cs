using BaseSource.Shared.Constants;
using BaseSource.ViewModels.Common;
using BaseSource.Shared.Extensions;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using BaseSource.ViewModels.Upload;

namespace BaseSource.Services.Uploads
{
    public class UploadService : IUploadService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public UploadService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public string CombineHostUrl(string filePath)
        {
            string uploadHost = _configuration["UploadApiEndpoint"];
            return string.IsNullOrEmpty(filePath) ? null : uploadHost + filePath;
        }

        //public async Task<ApiResult<string>> UploadFile(UploadFileVm model)
        //{
        //    var client = _httpClientFactory.CreateClient(SystemConstants.AppSettings.UploadApiClient);
        //    var multiContent = new MultipartFormDataContent();
        //    if (model.File != null)
        //    {
        //        byte[] data;
        //        using (var br = new BinaryReader(model.File.OpenReadStream()))
        //        {
        //            data = br.ReadBytes((int)model.File.OpenReadStream().Length);
        //        }
        //        ByteArrayContent bytes = new ByteArrayContent(data);
        //        bytes.Headers.ContentType = MediaTypeHeaderValue.Parse(model.File.ContentType);

        //        multiContent.Add(bytes, "File", model.File.FileName);
        //    }
        //    multiContent.Add(new StringContent(model.FileType.ToString()), "FileType");
        //    multiContent.Add(new StringContent(model.DirType.ToString()), "DirType");

        //    using (var response = await client.PostAsync("api/file/uploadfile", multiContent))
        //    {
        //        var responseString = await response.Content.ReadAsStringAsync();
        //        var result = JsonConvert.DeserializeObject<ApiResult<string>>(responseString);
        //        return result;
        //    }
        //}

        public async Task<string> UploadFile(UploadFileVm model)
        {
            var client = _httpClientFactory.CreateClient(SystemConstants.UploadApiClient);
            var multiContent = new MultipartFormDataContent();
            if (model.File != null)
            {
                var fileStreamContent = new StreamContent(model.File.OpenReadStream());
                fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue(model.File.ContentType);
                multiContent.Add(fileStreamContent, "File", model.File.FileName);
            }
            multiContent.Add(new StringContent(model.FileType.ToString()), "FileType");
            multiContent.Add(new StringContent(model.DirType.ToString()), "DirType");

            using (var response = await client.PostAsync("api/file/uploadfile", multiContent))
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ApiResult<string>>(responseString);
                return result?.ResultObj;
            }
        }

        public async Task<List<KeyValuePair<bool, string>>> UploadFiles(UploadFilesVm model)
        {
            var client = _httpClientFactory.CreateClient(SystemConstants.UploadApiClient);
            var multiContent = new MultipartFormDataContent();

            for (int i = 0; i < model.Files.Count; i++)
            {
                if (model.Files[i] != null)
                {
                    var fileStreamContent = new StreamContent(model.Files[i].OpenReadStream());
                    fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue(model.Files[i].ContentType);
                    multiContent.Add(fileStreamContent, $"Files", model.Files[i].FileName);
                }
            }

            multiContent.Add(new StringContent(model.FileType.ToString()), "FileType");
            multiContent.Add(new StringContent(model.DirType.ToString()), "DirType");

            using (var response = await client.PostAsync("api/file/uploadfiles", multiContent))
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<KeyValuePair<bool, string>>>(responseString);
                return result;
            }
        }

        public async Task<string> DeleteFile(DeleteFileVm model)
        {
            var client = _httpClientFactory.CreateClient(SystemConstants.UploadApiClient);
            return await client.PostAsync<string>("/api/file/DeleteFile", model);
        }

        public async Task<List<KeyValuePair<bool, string>>> DeleteFiles(DeleteFilesVm model)
        {
            var client = _httpClientFactory.CreateClient(SystemConstants.UploadApiClient);
            return await client.PostAsync<List<KeyValuePair<bool, string>>>("/api/file/DeleteFiles", model);
        }
    }
}
