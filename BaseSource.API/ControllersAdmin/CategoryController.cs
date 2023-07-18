using BaseSource.Services.Services.Category;
using BaseSource.ViewModels.Common;
using BaseSource.ViewModels.Project;
using EFCore.UnitOfWork.PageList;
using Microsoft.AspNetCore.Mvc;

namespace BaseSource.API.ControllersAdmin
{
    public class CategoryController : BaseAdminApiController
    {
        private readonly ICategoryProjectService _categoryProjectService;
        public CategoryController(
            ICategoryProjectService categoryProjectService
            )
        {
            _categoryProjectService = categoryProjectService;
        }

        [HttpGet]
        [Route("/api/admin/categorys")]
        public async Task<IActionResult> GetAll([FromQuery] CategoryRequestDto model)
        {
            var result = await _categoryProjectService.GetAllByFilterAsync(model);
            return Ok(new ApiSuccessResult<IPagedList<CategoryInfoDto>>(result));
        }

        [HttpGet]
        [Route("{id:int:min(1)}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _categoryProjectService.GetByIdAsync(id);
            return Ok(new ApiSuccessResult<CategoryInfoDto>(result));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDto model)
        {
            var result = await _categoryProjectService.CreateAsync(model);
            if (!result.Key)
            {
                return Ok(new ApiErrorResult<string>(result.Value.ToString()));
            }
            return Ok(new ApiSuccessResult<object>(result.Value));
        }

        [HttpPut]
        [Route("{id:int:min(1)}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryUpdateDto model)
        {
            var result = await _categoryProjectService.UpdateAsync(id, model);
            if (!result.Key)
            {
                return Ok(new ApiErrorResult<string>(result.Value.ToString()));
            }
            return Ok(new ApiSuccessResult<object>(result.Value));
        }

        [HttpPut]
        [Route("{id:int:min(1)}/published")]
        public async Task<IActionResult> Punlished(int id)
        {
            var result = await _categoryProjectService.PublishedAsync(id);
            if (!result.Key)
            {
                return Ok(new ApiErrorResult<string>(result.Value.ToString()));
            }
            return Ok(new ApiSuccessResult<object>(result.Value));
        }

        [HttpDelete]
        [Route("{id:int:min(1)}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryProjectService.DeleteAsync(id);
            if (!result.Key)
            {
                return Ok(new ApiErrorResult<string>(result.Value.ToString()));
            }
            return Ok(new ApiSuccessResult<object>(result.Value));
        }
    }
}
