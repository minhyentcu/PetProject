using BaseSource.ApiIntegration.WebApi.Category;
using BaseSource.ApiIntegration.WebApi.Project;
using BaseSource.Shared.Constants;
using BaseSource.ViewModels.Project;
using Microsoft.AspNetCore.Mvc;

namespace BaseSource.AppUI.Areas.Admin.Controllers
{
    public class ProjectController : BaseAdminController
    {
        private readonly IProjectAdminApiClient _projectAdminApiClient;
        private readonly ICategoryService _categoryService;
        public ProjectController(
            IProjectAdminApiClient projectAdminApiClient
            , ICategoryService categoryService
            )
        {
            _projectAdminApiClient = projectAdminApiClient;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _projectAdminApiClient.GetAllByFilterAsync(new PetProjectRequestDto
            {
                Page = 1,
                PageSize = 800
            });
            if (result == null || result.ResultObj == null)
            {
                return NotFound();
            }
            return View(result.ResultObj.Items);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categorys = await _categoryService.GetAllByFilterAsync(new CategoryRequestDto
            {
                Page = 1,
                PageSize = 200
            });
            if (categorys == null || categorys.ResultObj == null)
            {
                return NotFound();
            }
            ViewBag.Categorys = categorys.ResultObj.Items;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Info(int id)
        {
            if (id < 1)
            {
                return NotFound();
            }
            var projectInfo = await _projectAdminApiClient.GetByIdAsync(id);
            if (projectInfo == null || projectInfo.ResultObj == null)
            {
                return NotFound();
            }
            var categorys = await _categoryService.GetAllByFilterAsync(new CategoryRequestDto
            {
                Page = 1,
                PageSize = 200
            });
            if (categorys == null || categorys.ResultObj == null)
            {
                return NotFound();
            }
            ViewBag.Categorys = categorys.ResultObj.Items;
            return View(projectInfo.ResultObj);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PetProjectCreateDto model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Keys
                    .Where(k => ModelState[k].Errors.Count > 0)
                    .Select(k => new
                    {
                        propertyName = k,
                        errorMessage = ModelState[k].Errors[0].ErrorMessage
                    }).ToList();
                if (errors.Any(x => x.errorMessage == "CategoryProjectId"))
                {
                    ModelState.AddModelError("CategoryProjectId", "Danh mục không được để trống, hoặc không hợp lệ!");
                }

                var categorys = await _categoryService.GetAllByFilterAsync(new CategoryRequestDto
                {
                    Page = 1,
                    PageSize = 200,
                    IsAll = true
                });
                if (categorys == null || categorys.ResultObj == null)
                {
                    return NotFound();
                }
                ViewBag.Categorys = categorys.ResultObj.Items;
                return View(model);
            }

            if (model.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Hình ảnh không được để trống, hoặc không hợp lệ!");
            }
            var result = await _projectAdminApiClient.CreateAsync(model);
            if (result == null || !result.IsSuccessed)
            {

                ModelState.AddModelError("", result?.Message ?? "Tạo mới Project không thành công!");
                var categorys = await _categoryService.GetAllByFilterAsync(new CategoryRequestDto
                {
                    Page = 1,
                    PageSize = 200,
                    IsAll = true
                });
                if (categorys == null || categorys.ResultObj == null)
                {
                    return NotFound();
                }
                ViewBag.Categorys = categorys.ResultObj.Items.ToList();
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _projectAdminApiClient.DeleteAsync(id);
            if (result == null || !result.IsSuccessed)
            {
                return Json(new { code = ErrorCodeJson.ErrorMess, err = result?.Message ?? "Cập nhật không thành công!" });
            }
            return Json(new { code = ErrorCodeJson.Success });
        }
        [HttpPost]
        public async Task<IActionResult> Published(int id)
        {
            var result = await _projectAdminApiClient.PublishedAsync(id);
            if (result == null || !result.IsSuccessed)
            {
                return Json(new { code = ErrorCodeJson.ErrorMess, err = result?.Message ?? "Cập nhật không thành công!" });
            }
            return Json(new { code = ErrorCodeJson.Success });
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            if (id < 1)
            {
                return NotFound();
            }
            var projectInfo = await _projectAdminApiClient.GetByIdAsync(id);
            if (projectInfo == null || projectInfo.ResultObj == null)
            {
                return NotFound();
            }
            var categorys = await _categoryService.GetAllByFilterAsync(new CategoryRequestDto
            {
                Page = 1,
                PageSize = 200,
                IsAll = true
            });
            if (categorys == null || categorys.ResultObj == null)
            {
                return NotFound();
            }
            ViewBag.Categorys = categorys.ResultObj.Items;
            return View(new PetProjectUpdateDto
            {
                Id = projectInfo.ResultObj.Id,
                CategoryProjectId = projectInfo.ResultObj.CategoryProjectId,
                Image = projectInfo.ResultObj.Image,
                LinkDemo = projectInfo.ResultObj.LinkDemo,
                LinkSourceCode = projectInfo.ResultObj.LinkSourceCode,
                Name = projectInfo.ResultObj.Name,
                Description = projectInfo.ResultObj.Description,
            });
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, PetProjectUpdateDto model)
        {
            if (!ModelState.IsValid)
            {
                var categorys = await _categoryService.GetAllByFilterAsync(new CategoryRequestDto
                {
                    Page = 1,
                    PageSize = 200,
                    IsAll = true
                });
                if (categorys == null || categorys.ResultObj == null)
                {
                    return NotFound();
                }
                ViewBag.Categorys = categorys.ResultObj.Items;
                return View(model);
            }
            var result = await _projectAdminApiClient.UpdateAsync(id, model);
            if (result == null || !result.IsSuccessed)
            {

                ModelState.AddModelError("", result?.ResultObj ?? "Cập nhật Project không thành công!");
                var categorys = await _categoryService.GetAllByFilterAsync(new CategoryRequestDto
                {
                    Page = 1,
                    PageSize = 200,
                    IsAll = true
                });
                if (categorys == null || categorys.ResultObj == null)
                {
                    return NotFound();
                }
                ViewBag.Categorys = categorys.ResultObj.Items;
                return View(model);
            }
            return RedirectToAction("Index");
        }
    }
}
