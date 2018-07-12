using Autofac;
using SqlHelpCore;
using SqlHelpCore.Application.Interface;
using SqlHelpCore.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.HX01;
using WebApplication1.Nopi;

namespace WebApplication1.Controllers
{
    public class HX01Controller : BaseController
    {
        // GET: HX01
        public ActionResult HX010100()
        {
            return View();
        }

        public string GetList(ChenkPageModelcs model, int page, int rows)
        {
            var adminService = Container.Instance.Resolve<IAdministratorService>();
            var list = adminService.GetList(model.build(), page, rows);
            return Success(list.TotalRecords, list.Data);
        }

        public string AddAdmin(AdminModel model)
        {
            if (ModelState.IsValid)
            {
                var adminService = Container.Instance.Resolve<IAdministratorService>();
                User user = new User()
                {                 
                    UserName = model.LoginName,
                    RealName = model.Name,
                    PassWard = model.passWard,
                    Email = model.Email,
                    Birthday = model.birthday,
                    Sex = model.sex,
                    CreateTime = DateTime.Now,
                    Qq = model.Qq,
                    Moblie = model.Mobile
                };           
                if (model.Id == null)
                {
                    if (adminService.AddAdmin(user))
                    {
                        return Success("成功");
                    }
                    return Error("失败");
                }
                else
                {
                    user.Id = model.Id.Value;
                    if (adminService.EditAdmin(user))
                    {
                        return Success("成功");
                    }
                    return Error("失败");
                }                       
            }
            else
            {
                return Error(GetFirstErrorMessage());
            }
        }

        public string GetUser(int id)
        {
            var adminService = Container.Instance.Resolve<IAdministratorService>();
            var model = adminService.GetUser(id);
            return Success(model);
        }

        public void Excel(ChenkPageModelcs model)
        {
            var adminService = Container.Instance.Resolve<IAdministratorService>();
            var list = adminService.Excel(model.build());         
            Dictionary<string, string> cellhead = new Dictionary<string, string>()
            {
                {"UserName","登录名"}, {"RealName","真实姓名"},{"Moblie", "手机号"}, {"Email", "邮箱"},
                {"CreateTime","注册时间"}, {"Birthday","出生日期"}, {"Qq","qq"}, {"Sex","性别"}
            };
            ExcelNpoi npe = new ExcelNpoi();
            npe.NOPIEntityListToExcel(cellhead, list, "用户管理", "用户管理");        
        }
    }
}