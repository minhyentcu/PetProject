using BaseSource.Services.Services.Project;
using BaseSource.ViewModels.Common;
using BaseSource.ViewModels.Project;
using EFCore.UnitOfWork.PageList;
using Microsoft.AspNetCore.Mvc;

namespace BaseSource.API.ControllersAdmin
{
    public class ProjectController : BaseAdminApiController
    {
        private readonly IPetProjectService _petProjectService;
        public ProjectController(
            IPetProjectService petProjectService
            )
        {
            _petProjectService = petProjectService;
        }

        [HttpGet]
        [Route("/api/admin/projects")]
        public async Task<IActionResult> GetAll([FromQuery] PetProjectRequestDto model)
        {
            var result = await _petProjectService.GetByFilterAsync(model);
            return Ok(new ApiSuccessResult<IPagedList<PetProjectDto>>(result));
        }

        [HttpGet]
        [Route("{id:int:min(1)}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _petProjectService.GetByIdAsync(id);
            return Ok(new ApiSuccessResult<PetProjectDto>(result));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] PetProjectCreateDto model)
        {
            var result = await _petProjectService.CreateAsync(model);
            if (!result.Key)
            {
                return Ok(new ApiErrorResult<string>(result.Value.ToString()));
            }
            return Ok(new ApiSuccessResult<object>(result.Value));
        }

        [HttpPut]
        [Route("{id:int:min(1)}")]
        public async Task<IActionResult> Update(int id, [FromForm] PetProjectUpdateDto model)
        {
            var result = await _petProjectService.UpdateAsync(id, model);
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
            var result = await _petProjectService.PublishedAsync(id);
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
            var result = await _petProjectService.DeletedAsync(id);
            if (!result.Key)
            {
                return Ok(new ApiErrorResult<string>(result.Value.ToString()));
            }
            return Ok(new ApiSuccessResult<object>(result.Value));
        }
    }
}
