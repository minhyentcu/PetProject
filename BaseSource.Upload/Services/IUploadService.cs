using BaseSource.Shared.Enums;
using BaseSource.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseSource.UploadApi.Services
{
    public interface IUploadService
    {
        Task<ApiResult<string>> UploadFile(IFormFile file, EUploadFileType fileType, EUploadDirectoryType dirType);
        Task<List<KeyValuePair<bool, string>>> UploadFiles(List<IFormFile> files, EUploadFileType fileType, EUploadDirectoryType dirType);
        ApiResult<string> DeleteFile(string filePath);
        List<KeyValuePair<bool, string>> DeleteFiles(List<string> filePaths);
    }
}
