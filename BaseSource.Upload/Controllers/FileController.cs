using BaseSource.UploadApi.Services;
using BaseSource.ViewModels.Common;
using BaseSource.ViewModels.Upload;
using Microsoft.AspNetCore.Mvc;

namespace BaseSource.Upload.Controllers
{
    [Route("api/[controller]")]
    public class FileController : BaseApiController
    {
        private readonly IUploadService _fileService;
        public FileController(IUploadService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile([FromForm] UploadFileVm model)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new ApiErrorResult<string>(ModelState.GetListErrors()));
            }

            var result = await _fileService.UploadFile(model.File, model.FileType, model.DirType);

            return Ok(result);
        }

        [HttpPost("UploadFiles")]
        public async Task<IActionResult> UploadFiles([FromForm] UploadFilesVm model)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new ApiErrorResult<List<KeyValuePair<bool, string>>>(ModelState.GetListErrors()));
            }

            if (model.Files == null || model.Files.Count == 0)
            {
                return Ok(new ApiErrorResult<List<KeyValuePair<bool, string>>>("Danh sách trống"));
            }

            var result = await _fileService.UploadFiles(model.Files, model.FileType, model.DirType);

            return Ok(new ApiSuccessResult<List<KeyValuePair<bool, string>>>(result));
        }

        [HttpPost("DeleteFile")]
        public IActionResult DeleteFile(DeleteFileVm model)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new ApiErrorResult<string>(ModelState.GetListErrors()));
            }

            var result = _fileService.DeleteFile(model.FilePath);

            return Ok(result);
        }

        [HttpPost("DeleteFiles")]
        public IActionResult DeleteFiles(DeleteFilesVm model)
        {
            if (model.FilePaths == null || model.FilePaths.Count == 0)
            {
                return Ok(new ApiErrorResult<string>("Danh sách trống"));
            }

            var result = _fileService.DeleteFiles(model.FilePaths);

            return Ok(new ApiSuccessResult<List<KeyValuePair<bool, string>>>(result));
        }
    }
}
