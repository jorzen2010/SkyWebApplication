using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SkyWebApplication.ViewModels
{
    public class SysUserViewModel
    {

        public  int ID { get; set; }
        [Required]
        [Display(Name = "用户名")]
        public  string UserName { get; set; }
        [Required]
        [Display(Name = "密码")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "邮箱")]
        public string Email { get; set; }
    }
}