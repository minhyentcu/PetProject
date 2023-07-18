using BaseSource.Data.Entities;
using BaseSource.Services.Uploads;
using BaseSource.Shared.Enums;
using BaseSource.Shared.Helpers;
using BaseSource.ViewModels.Project;
using BaseSource.ViewModels.Upload;
using EFCore.UnitOfWork;
using EFCore.UnitOfWork.PageList;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BaseSource.Services.Services.Project
{
    public class PetProjectService : IPetProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PetProjectService> _logger;
        private readonly IUploadService _uploadService;
        public PetProjectService(IUnitOfWork unitOfWork
            , ILogger<PetProjectService> logger
            , IUploadService uploadService
            )
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _uploadService = uploadService;

        }
        public async Task<KeyValuePair<bool, string>> CreateAsync(PetProjectCreateDto model)
        {
            try
            {
                var _repository = _unitOfWork.GetRepository<PetProject>();
                var name = model.Name.Trim().ToLower();

                var slugProject = UrlHelper.GenerateSlug(model.Name);
                var projectCurrent = await _repository.GetFirstOrDefaultAsync(
                    predicate: x => x.Slug == slugProject || x.Name.ToLower() == name);

                if (projectCurrent != null)
                {
                    return new KeyValuePair<bool, string>(false, "Project đã tồn tại!");
                }

                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    var uploadRs = await _uploadService.UploadFile(new UploadFileVm()
                    {
                        File = model.ImageFile,
                        FileType = EUploadFileType.Image,
                        DirType = EUploadDirectoryType.Project
                    });

                    if (string.IsNullOrEmpty(uploadRs))
                    {
                        return new KeyValuePair<bool, string>(false, "Hình ảnh không hợp lệ!");
                    }
                    model.Image = uploadRs;
                }

                _repository.Insert(new PetProject
                {
                    Name = model.Name,
                    CreatedTime = DateTime.Now,
                    LinkDemo = model.LinkDemo,
                    LinkSourceCode = model.LinkSourceCode,
                    Description = model.Description,
                    Published = true,
                    Image = model.Image,
                    CategoryProjectId = model.CategoryProjectId,
                });
                await _unitOfWork.SaveChangesAsync();
                return new KeyValuePair<bool, string>(true, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Create Project failed by error:[{ex}]");
                return new KeyValuePair<bool, string>(false, $"Tạo project không thành công:[{ex.Message}]");
            }
        }

        public async Task<KeyValuePair<bool, string>> DeletedAsync(int id)
        {
            try
            {
                var _repository = _unitOfWork.GetRepository<PetProject>();
                var projectEntity = await _repository.GetFirstOrDefaultAsync(
                    predicate: x => x.Id == id);
                if (projectEntity == null)
                {
                    return new KeyValuePair<bool, string>(false, "Project không tồn tại!");
                }
                projectEntity.DeletedTime = DateTime.Now;
                _repository.Update(projectEntity);
                await _unitOfWork.SaveChangesAsync();
                return new KeyValuePair<bool, string>(true, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Delete Project failed by error:[{ex}]");
                return new KeyValuePair<bool, string>(false, $"Xóa project không thành công:[{ex.Message}]");
            }
        }

        public async Task<IPagedList<PetProjectDto>> GetByFilterAsync(PetProjectRequestDto model)
        {
            var _repository = _unitOfWork.GetRepository<PetProject>();
            var query = _repository.Queryable().AsNoTracking();
            if (!string.IsNullOrEmpty(model.Name))
            {
                model.Name = model.Name.Trim().ToLower();
                query = query.Where(x => x.Name.ToLower() == model.Name);
            }
            return await _repository.GetStagesPagedListAsync(
                stages: query,
                selector: x => new PetProjectDto
                {
                    Description = x.Description,
                    Id = x.Id,
                    Image = _uploadService.CombineHostUrl(x.Name),
                    LinkDemo = x.LinkDemo,
                    LinkSourceCode = x.LinkSourceCode,
                    Name = x.Name
                }, orderBy: x => x.OrderByDescending(i => i.CreatedTime),
                pageIndex: model.Page, pageSize: model.PageSize);
        }

        public async Task<PetProjectDto> GetByIdAsync(int id)
        {
            var _repository = _unitOfWork.GetRepository<PetProject>();
            return await _repository.GetFirstOrDefaultAsync(
                predicate: x => x.Id == id, disableTracking: true,
                selector: x => new PetProjectDto
                {
                    Description = x.Description,
                    Id = x.Id,
                    Image = _uploadService.CombineHostUrl(x.Name),
                    LinkDemo = x.LinkDemo,
                    LinkSourceCode = x.LinkSourceCode,
                    Name = x.Name
                });
        }

        public async Task<KeyValuePair<bool, string>> PublishedAsync(int id)
        {
            try
            {
                var _repository = _unitOfWork.GetRepository<PetProject>();
                var projectEntity = await _repository.GetFirstOrDefaultAsync(
                    predicate: x => x.Id == id);
                if (projectEntity == null)
                {
                    return new KeyValuePair<bool, string>(false, "Project không tồn tại!");
                }
                projectEntity.Published = !projectEntity.Published;
                _repository.Update(projectEntity);
                await _unitOfWork.SaveChangesAsync();
                return new KeyValuePair<bool, string>(true, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Delete Project failed by error:[{ex}]");
                return new KeyValuePair<bool, string>(false, $"Xóa project không thành công:[{ex.Message}]");
            }
        }

        public async Task<KeyValuePair<bool, string>> UpdateAsync(int id, PetProjectUpdateDto model)
        {
            try
            {
                var _repository = _unitOfWork.GetRepository<PetProject>();

                var slugProject = UrlHelper.GenerateSlug(model.Name);
                
                var projectEntity = await _repository.GetFirstOrDefaultAsync(
                    predicate: x => x.Id == id);
                if (projectEntity == null)
                {
                    return new KeyValuePair<bool, string>(false, "Project không tồn tại!");
                }
                var projectExitstTitle = await _repository.ExistsAsync(x => x.Id != id && x.Name.ToLower() == model.Name.ToLower());
                if (projectExitstTitle)
                {
                    return new KeyValuePair<bool, string>(false, "Tiêu đề bài viết đã tồn tại, vui lòng kiểm tra lại!");
                }

                projectEntity.Name = model.Name;
                projectEntity.Description = model.Description;
                projectEntity.LinkDemo = model.LinkDemo;
                projectEntity.LinkSourceCode = model.LinkSourceCode;
                projectEntity.Slug = slugProject;

                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    var uploadRs = await _uploadService.UploadFile(new UploadFileVm()
                    {
                        File = model.ImageFile,
                        FileType = EUploadFileType.Image,
                        DirType = EUploadDirectoryType.Project
                    });

                    if (string.IsNullOrEmpty(uploadRs))
                    {
                        return new KeyValuePair<bool, string>(false, "Hình ảnh không hợp lệ!");
                    }
                    model.Image = uploadRs;
                    projectEntity.Image = model.Image;
                }
                _repository.Update(projectEntity);
                await _unitOfWork.SaveChangesAsync();
                return new KeyValuePair<bool, string>(true, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Update Project failed by error:[{ex}]");
                return new KeyValuePair<bool, string>(false, $"Cập nhật project không thành công:[{ex.Message}]");
            }
        }
    }
}
