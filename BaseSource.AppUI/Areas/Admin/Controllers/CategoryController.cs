using BaseSource.ApiIntegration.WebApi.Category;
using BaseSource.Shared.Constants;
using BaseSource.ViewModels.Project;
using Microsoft.AspNetCore.Mvc;

namespace BaseSource.AppUI.Areas.Admin.Controllers
{
    public class CategoryController : BaseAdminController
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(
            ICategoryService categoryService
            )
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.GetAllByFilterAsync(new CategoryRequestDto
            {
                Page = 1,
                PageSize = 100
            });
            if (result == null)
            {
                return NotFound();
            }
            return View(result.ResultObj?.Items?.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateDto model)
        {
            var result = await _categoryService.CreateAsync(model);
            if (result == null || !result.IsSuccessed)
            {
                return Json(new { code = ErrorCodeJson.ErrorMess, err = result?.Message ?? "Tạo mới không thành công!" });
            }
            return Json(new { code = ErrorCodeJson.Success });
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, CategoryUpdateDto model)
        {
            var result = await _categoryService.UpdateAsync(id, model);
            if (result == null || !result.IsSuccessed)
            {
                return Json(new { code = ErrorCodeJson.ErrorMess, err = result?.Message ?? "Cập nhật không thành công!" });
            }
            return Json(new { code = ErrorCodeJson.Success });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryService.DeleteAsync(id);
            if (result == null || !result.IsSuccessed)
            {
                return Json(new { code = ErrorCodeJson.ErrorMess, err = result?.Message ?? "Xóa không thành công!" });
            }
            return Json(new { code = ErrorCodeJson.Success });
        }
        [HttpPost]
        public async Task<IActionResult> Published(int id)
        {
            var result = await _categoryService.PublishedAsync(id);
            if (result == null || !result.IsSuccessed)
            {
                return Json(new { code = ErrorCodeJson.ErrorMess, err = result?.Message ?? "Cập nhật không thành công!" });
            }
            return Json(new { code = ErrorCodeJson.Success });
        }
    }
}
