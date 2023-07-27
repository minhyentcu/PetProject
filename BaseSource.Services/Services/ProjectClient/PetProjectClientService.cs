using BaseSource.Data.Entities;
using BaseSource.Services.Uploads;
using BaseSource.Shared.Enums;
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
            query = query.Where(x => x.DeletedTime == null && x.Published && x.CategoryProject.Published && x.CategoryProject.DeletedTime == null);
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
                    Image = _uploadService.CombineHostUrl(x.Image),
                    LinkDemo = x.LinkDemo,
                    LinkSourceCode = x.LinkSourceCode,
                    Name = x.Name.Trim(),
                    Slug = x.Slug,
                    CreatedTime = x.CreatedTime,
                    CategoryName = x.CategoryProject.Name,
                    TotalVoteUp = x.VoteProjects.Where(x => x.Status == VoteStatus.Up).Count(),
                    TotalVoteDown = x.VoteProjects.Where(x => x.Status == VoteStatus.Down).Count(),
                    VoteUp = x.VoteProjects.Any(x => x.IP == model.IP && x.Status == VoteStatus.Up),
                    VoteDown = x.VoteProjects.Any(x => x.IP == model.IP && x.Status == VoteStatus.Down),
                }, orderBy: x => x.OrderByDescending(i => i.CreatedTime),
                pageIndex: model.Page, pageSize: model.PageSize);
        }

        public async Task<PetProjectDto> GetByIdAsync(string slug, string ip)
        {
            var _repository = _unitOfWork.GetRepository<PetProject>();
            return await _repository.GetFirstOrDefaultAsync(
                predicate: x => x.Slug == slug && x.DeletedTime == null && x.Published, disableTracking: true,
                selector: x => new PetProjectDto
                {
                    Description = x.Description,
                    Id = x.Id,
                    Image = _uploadService.CombineHostUrl(x.Image),
                    LinkDemo = x.LinkDemo,
                    LinkSourceCode = x.LinkSourceCode,
                    Name = x.Name,
                    Slug = x.Slug,
                    CategoryName = x.CategoryProject.Name,
                    CreatedTime = x.CreatedTime,
                    TotalVoteUp = x.VoteProjects.Where(x => x.Status == VoteStatus.Up).Count(),
                    TotalVoteDown = x.VoteProjects.Where(x => x.Status == VoteStatus.Down).Count(),
                    VoteUp = x.VoteProjects.Any(x => x.IP == ip && x.Status == VoteStatus.Up),
                    VoteDown = x.VoteProjects.Any(x => x.IP == ip && x.Status == VoteStatus.Down),
                });
        }

        public async Task<KeyValuePair<bool, string>> VotingAsync(VoteProjectUpdateDto model)
        {
            try
            {
                var _repository = _unitOfWork.GetRepository<VoteProject>();

                var dataVote = await _repository.GetFirstOrDefaultAsync(
                    predicate: x => x.PetProjectId == model.ProjectId && x.IP == model.IP);

                if (dataVote != null)
                {
                    if ((model.IsDown && dataVote.Status == VoteStatus.Down)
                        || (model.IsUp && dataVote.Status == VoteStatus.Up))
                    {
                        _repository.Delete(dataVote);
                    }
                    else
                    {
                        dataVote.Status = model.IsDown ? VoteStatus.Down : VoteStatus.Up;
                        _repository.Update(dataVote);
                    }
                }
                else
                {
                    _repository.Insert(new VoteProject
                    {
                        PetProjectId = model.ProjectId,
                        IP = model.IP,
                        Status = model.IsDown ? VoteStatus.Down : VoteStatus.Up
                    });
                }
                await _unitOfWork.SaveChangesAsync();
                return new KeyValuePair<bool, string>(true, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Voting Project [{model.ProjectId}] failed by error:[{ex}]");

                return new KeyValuePair<bool, string>(false, $"Lỗi Vote [{ex.Message}]");
            }

        }
    }
}

