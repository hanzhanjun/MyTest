using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.HX01
{
    public class AdminModel
    {
        [Required(ErrorMessage ="姓名不能为空")]
        public string Name { get; set; }

        [Required(ErrorMessage = "手机号不能为空")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "出生日期不能为空")]
        public DateTime birthday { get; set; }

        [Required(ErrorMessage = "登录名不能为空")]
        public string LoginName { get; set; }

        public string Qq { get; set; }

        public string Email { get; set; }

        [Required(ErrorMessage = "密码不能为空")]
        public string passWard { get; set; }

        [Required(ErrorMessage = "性别不能为空")]
        public int sex { get; set; }   
        
        public int? Id { get; set; }   
    }
}