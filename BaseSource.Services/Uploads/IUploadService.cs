using BaseSource.ViewModels.Upload;

namespace BaseSource.Services.Uploads
{
    public interface IUploadService
    {
        string CombineHostUrl(string filePath);
        Task<string> UploadFile(UploadFileVm model);
        Task<List<KeyValuePair<bool, string>>> UploadFiles(UploadFilesVm model);
        Task<string> DeleteFile(DeleteFileVm model);
        Task<List<KeyValuePair<bool, string>>> DeleteFiles(DeleteFilesVm model);
    }
}
