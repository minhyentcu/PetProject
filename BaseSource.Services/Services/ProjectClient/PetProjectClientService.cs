using BaseSource.Data.Entities;
using BaseSource.Services.Uploads;
using BaseSource.ViewModels.Project;
using EFCore.UnitOfWork;
using EFCore.UnitOfWork.PageList;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSource.Services.Services.ProjectClient
{
    public class PetProjectClientService : IPetProjectClientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PetProjectClientService> _logger;
        private readonly IUploadService _uploadService;
        public PetProjectClientService(IUnitOfWork unitOfWork
            , ILogger<PetProjectClientService> logger
            , IUploadService uploadService
            )
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _uploadService = uploadService;

        }
        public async Task<IPagedList<PetProjectDto>> GetAllByFilterAsync(ProjectClientRequestDto model)
        {
            var _repository = _unitOfWork.GetRepository<PetProject>();
            var query = _repository.Queryable().AsNoTracking();
            if (string.IsNullOrEmpty(model.Name))
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
                    Name = x.Name,
                    Slug = x.Slug,
                }, orderBy: x => x.OrderByDescending(i => i.CreatedTime),
                pageIndex: model.Page, pageSize: model.PageSize);
        }

        public async Task<PetProjectDto> GetByIdAsync(string slug)
        {
            var _repository = _unitOfWork.GetRepository<PetProject>();
            return await _repository.GetFirstOrDefaultAsync(
                predicate: x => x.Slug == slug, disableTracking: true,
                selector: x => new PetProjectDto
                {
                    Description = x.Description,
                    Id = x.Id,
                    Image = _uploadService.CombineHostUrl(x.Name),
                    LinkDemo = x.LinkDemo,
                    LinkSourceCode = x.LinkSourceCode,
                    Name = x.Name,
                    Slug = x.Slug
                });
        }
    }
}
