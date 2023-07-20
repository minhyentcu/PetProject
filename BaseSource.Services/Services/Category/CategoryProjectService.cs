using BaseSource.Data.Entities;
using BaseSource.Shared.Helpers;
using BaseSource.ViewModels.Project;
using EFCore.UnitOfWork;
using EFCore.UnitOfWork.PageList;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BaseSource.Services.Services.Category
{
    public class CategoryProjectService : ICategoryProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CategoryProjectService> _logger;

        public CategoryProjectService(
            IUnitOfWork unitOfWork,
            ILogger<CategoryProjectService> logger
            )
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<KeyValuePair<bool, string>> CreateAsync(CategoryCreateDto model)
        {
            try
            {
                var _repository = _unitOfWork.GetRepository<CategoryProject>();

                var name = model.Name.Trim().ToLower();

                var slugCategory = UrlHelper.GenerateSlug(name);
                var categoryEntity = await _repository.GetFirstOrDefaultAsync(
                    predicate: x => x.Name.ToLower() == name);
                if (categoryEntity != null)
                {
                    return new KeyValuePair<bool, string>(false, "Danh mục đã tồn tại!");
                }
                _repository.Insert(new CategoryProject
                {
                    Name = model.Name.Trim(),
                    CreatedTime = DateTime.Now,
                    Published = true,
                    Slug = slugCategory
                });
                await _unitOfWork.SaveChangesAsync();
                return new KeyValuePair<bool, string>(true, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Create CategoryProject failed by error:[{ex}]");
                return new KeyValuePair<bool, string>(false, $"Tạo mới không thành công:[{ex.Message}");
            }
        }

        public async Task<KeyValuePair<bool, string>> DeleteAsync(int id)
        {
            try
            {
                var _repository = _unitOfWork.GetRepository<CategoryProject>();

                var categoryEntity = await _repository.GetFirstOrDefaultAsync(
                    predicate: x => x.Id == id);
                if (categoryEntity == null)
                {
                    return new KeyValuePair<bool, string>(false, "Danh mục không tồn tại!");
                }
                categoryEntity.DeletedTime = DateTime.Now;
                _repository.Update(categoryEntity);
                await _unitOfWork.SaveChangesAsync();
                return new KeyValuePair<bool, string>(true, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Delete CategoryProject failed by error:[{ex}]");
                return new KeyValuePair<bool, string>(false, $"Xóa danh mục không thành công:[{ex.Message}");
            }
        }

        public async Task<IPagedList<CategoryInfoDto>> GetAllByFilterAsync(CategoryRequestDto model)
        {
            var _repository = _unitOfWork.GetRepository<CategoryProject>();

            var query = _repository.Queryable().AsNoTracking();

            query = query.Where(x => x.DeletedTime == null);
            if (!string.IsNullOrEmpty(model.Name))
            {
                model.Name = model.Name.Trim();
            }
            if (model.IsAll)
            {
                query = query.Where(x => x.Published);
            }
            return await _repository.GetStagesPagedListAsync(
                stages: query,
                selector: x => new CategoryInfoDto
                {
                    Name = x.Name,
                    Id = x.Id,
                    Published = x.Published,
                    CreatedTime = x.CreatedTime
                }, orderBy: x => x.OrderByDescending(i => i.CreatedTime),
                pageIndex: model.Page, pageSize: model.PageSize);
        }

        public async Task<CategoryInfoDto> GetByIdAsync(int id)
        {
            var _repository = _unitOfWork.GetRepository<CategoryProject>();

            return await _repository.GetFirstOrDefaultAsync(
                predicate: x => x.Id == id && x.DeletedTime == null && x.Published, disableTracking: true,
                selector: x => new CategoryInfoDto
                {
                    Name = x.Name,
                    Id = x.Id,
                    Published = x.Published,
                    CreatedTime = x.CreatedTime,
                });
        }

        public async Task<KeyValuePair<bool, string>> PublishedAsync(int id)
        {
            try
            {
                var _repository = _unitOfWork.GetRepository<CategoryProject>();

                var categoryEntity = await _repository.GetFirstOrDefaultAsync(
                    predicate: x => x.Id == id);
                if (categoryEntity == null)
                {
                    return new KeyValuePair<bool, string>(false, "Danh mục không tồn tại!");
                }
                categoryEntity.Published = !categoryEntity.Published;
                _repository.Update(categoryEntity);
                await _unitOfWork.SaveChangesAsync();
                return new KeyValuePair<bool, string>(true, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Delete CategoryProject failed by error:[{ex}]");
                return new KeyValuePair<bool, string>(false, $"Xóa danh mục không thành công:[{ex.Message}");
            }
        }

        public async Task<KeyValuePair<bool, string>> UpdateAsync(int id, CategoryUpdateDto model)
        {
            try
            {
                var _repository = _unitOfWork.GetRepository<CategoryProject>();
                var slugCategory = UrlHelper.GenerateSlug(model.Name);
                var categoryEntity = await _repository.GetFirstOrDefaultAsync(
                    predicate: x => x.Id == id);
                if (categoryEntity == null)
                {
                    return new KeyValuePair<bool, string>(false, "Danh mục không tồn tại!");
                }
                categoryEntity.Name = model.Name;
                categoryEntity.Slug = slugCategory;
                _repository.Update(categoryEntity);
                await _unitOfWork.SaveChangesAsync();
                return new KeyValuePair<bool, string>(true, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Update CategoryProject failed by error:[{ex}]");
                return new KeyValuePair<bool, string>(false, $"Cập nhật danh mục không thành công:[{ex.Message}");
            }
        }
    }
}
