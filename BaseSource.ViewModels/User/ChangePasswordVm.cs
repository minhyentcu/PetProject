﻿using System.ComponentModel.DataAnnotations;

namespace BaseSource.ViewModels.User
{
    public class ChangePasswordVm
    {
        [Required]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Mật khẩu ít nhất 6 ký tự", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Mật khẩu ít nhất 6 ký tự", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage ="Mật khẩu xác nhận không chính xác")]
        public string ConfirmPassword { get; set; }
    }
}
