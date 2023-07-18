﻿using BaseSource.Shared.Enums;
using BaseSource.UploadApi.Helper;
using BaseSource.ViewModels.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BaseSource.UploadApi.Services
{
    public class UploadService : IUploadService
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        public UploadService(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        public async Task<ApiResult<string>> UploadFile(IFormFile file, EUploadFileType fileType, EUploadDirectoryType dirType)
        {
            var rs = await UploadHelper.Upload(file, fileType, dirType, _hostEnvironment.WebRootPath);
            if (rs.Key)
            {
                return new ApiSuccessResult<string>(rs.Value);
            }
            return new ApiErrorResult<string>(rs.Value);
        }

        public async Task<List<KeyValuePair<bool, string>>> UploadFiles(List<IFormFile> files, EUploadFileType fileType, EUploadDirectoryType dirType)
        {
            var result = new List<KeyValuePair<bool, string>>();
            foreach (var file in files)
            {
                var rs = await UploadHelper.Upload(file, fileType, dirType, _hostEnvironment.WebRootPath);
                result.Add(rs);
            }

            return new List<KeyValuePair<bool, string>>(result);
        }

        public ApiResult<string> DeleteFile(string filePaths)
        {
            var rs = UploadHelper.RemoveFileFromServer(filePaths, _hostEnvironment.WebRootPath);
            if (rs.Key)
            {
                return new ApiSuccessResult<string>();
            }

            return new ApiErrorResult<string>(rs.Value);
        }

        public List<KeyValuePair<bool, string>> DeleteFiles(List<string> filePaths)
        {
            var result = new List<KeyValuePair<bool, string>>();
            foreach (var item in filePaths)
            {
                var rs = UploadHelper.RemoveFileFromServer(item, _hostEnvironment.WebRootPath);
                result.Add(rs);
            }
            return new List<KeyValuePair<bool, string>>(result);
        }
    }
}
