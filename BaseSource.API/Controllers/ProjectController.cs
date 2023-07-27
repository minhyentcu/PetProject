using BaseSource.Services.Services.ProjectClient;
using BaseSource.ViewModels.Common;
using BaseSource.ViewModels.Project;
using EFCore.UnitOfWork.PageList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseSource.API.Controllers
{
    public class ProjectController : BaseApiController
    {
        private readonly IPetProjectClientService _petProjectClientService;
        public ProjectController(IPetProjectClientService petProjectClientService)
        {
            _petProjectClientService = petProjectClientService;
        }
        [HttpGet]
        [Route("/api/projects")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] ProjectClientRequestDto model)
        {
            var result = await _petProjectClientService.GetAllByFilterAsync(model);
            return Ok(new ApiSuccessResult<IPagedList<PetProjectDto>>(result));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetBySlug(string slug, string ip)
        {
            var result = await _petProjectClientService.GetByIdAsync(slug, ip);
            return Ok(new ApiSuccessResult<PetProjectDto>(result));
        }

        [HttpPatch]
        [AllowAnonymous]
        [Route("voting")]
        public async Task<IActionResult> Voting([FromBody] VoteProjectUpdateDto model)
        {
            var result = await _petProjectClientService.VotingAsync(model);
            if (!result.Key)
            {
                return Ok(new ApiErrorResult<string>(result.Value.ToString()));
            }
            return Ok(new ApiSuccessResult<object>(result.Value));
        }
    }
}
