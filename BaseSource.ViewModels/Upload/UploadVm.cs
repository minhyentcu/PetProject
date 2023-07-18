using BaseSource.Shared.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BaseSource.ViewModels.Upload
{
    public class UploadFileVm
    {
        [Required]
        public EUploadFileType FileType { get; set; }

        [Required]
        public EUploadDirectoryType DirType { get; set; }

        [Required]
        public IFormFile File { get; set; }
    }

    public class UploadFilesVm
    {
        [Required]
        public EUploadFileType FileType { get; set; }

        [Required]
        public EUploadDirectoryType DirType { get; set; }

        public List<IFormFile> Files { get; set; }
    }

    public class DeleteFileVm
    {
        [Required]
        public string FilePath { get; set; }
    }

    public class DeleteFilesVm
    {
        public List<string> FilePaths { get; set; }
    }
}
