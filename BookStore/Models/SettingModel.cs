using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class SettingModel
    {
        [Required]
        public string Password { get; set; }


        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 letters")]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword", ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirm { get; set; }

    }
}