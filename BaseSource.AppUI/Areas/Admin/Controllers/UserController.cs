﻿
using BaseSource.ApiIntegration.WebApi.UserAdmin;
using BaseSource.ViewModels.UserAdmin;
using Microsoft.AspNetCore.Mvc;

namespace BaseSource.AppUI.Areas.Admin.Controllers
{
    public class UserController : BaseAdminController
    {
        private readonly IUserAdminApiClient _userAdminApiClient;


        public UserController(
            IUserAdminApiClient userAdminApiClient
           
            )
        {
            _userAdminApiClient = userAdminApiClient;
            

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _userAdminApiClient.GetUserByFilter(new UserAdminRequestDto
            {
                Page = 1,
                PageSize = 800,
                //UserName = searchValue
            });
            
            return View(result.ResultObj.Items);
        }

        [HttpPost]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) / pageSize : 0;
                int recordsTotal = 0;
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();

                var result = await _userAdminApiClient.GetUserByFilter(new UserAdminRequestDto
                {
                    Page = skip + 1,
                    PageSize = pageSize,
                    UserName = searchValue
                });

                recordsTotal = result.ResultObj.TotalCount;


                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = result.ResultObj.Items };
                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                return Json(new { countData = 0, datas = new List<UserAdminInfoDto>() });
            }
        }

        public async Task<IActionResult> Manager()
        {
            var result = await _userAdminApiClient.GetAllManager();
            if (result == null || !result.IsSuccessed)
            {
                return NotFound();
            }
            return View(result.ResultObj);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRoleManager(string userName)
        {
            var result = await _userAdminApiClient.UpdateRoleManager(userName);
            if (result == null || !result.IsSuccessed)
            {
                return RedirectToAction("Manager");
                //return Json(new { code = ErrorCodeJson.ErrorMess, err = result?.Message ?? "Cập nhật không thành công!" });
            }
            return RedirectToAction("Manager");
            // return Json(new { code = ErrorCodeJson.Success });
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordAdminDto model)
        {

            var result = await _userAdminApiClient.ResetPassword(model);
            if (result == null || !result.IsSuccessed)
            {
                return RedirectToAction("Index");
                //return Json(new { code = ErrorCodeJson.ErrorMess, err = result?.Message ?? "Cập nhật không thành công!" });
            }
            //return Json(new { code = ErrorCodeJson.Success, value=result.ResultObj });
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string userId)
        {
            var result = await _userAdminApiClient.Delete(userId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateAdminDto model)
        {
            var result = await _userAdminApiClient.Create(model);
            if (result == null || !result.IsSuccessed)
            {

                ModelState.AddModelError("", result?.ResultObj ?? "Tạo mới tài khoản không thành công");
                return View(model);
            }
            return RedirectToAction("Index");
        }
    }
}
