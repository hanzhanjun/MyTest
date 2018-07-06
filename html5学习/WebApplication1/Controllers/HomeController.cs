using Autofac;
using SqlHelpCore;
using SqlHelpCore.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Filter;
using WebApplication1.Models.home;

namespace WebApplication1.Controllers
{
    
    public class HomeController : BaseController
    {
        [UserAuthorize]
        public ActionResult Index()
        {       
            ViewData["ADMIN"] = CurrentUser.UserName;
            return View();
        }


        [UserAuthorize]
        public string GetMenu()
        {
            List<MenuModel> menumodel = new List<MenuModel>();
            var adminService = Container.Instance.Resolve<IAdministratorService>();
            var firstLevelMenus = adminService.GetAdminFirstMenu(CurrentUser.Id);

            if (firstLevelMenus != null && firstLevelMenus.Count > 0)
            {
                foreach (var firstLevelMenu in firstLevelMenus)
                {
                    MenuModel menuModel = new MenuModel();
                    menuModel.Id = firstLevelMenu.Id;
                    menuModel.Name = firstLevelMenu.Name;
                    menuModel.Url = firstLevelMenu.Url;
                    var secondLevelMenus = adminService.GetAdministratorsecondMenu(CurrentUser.Id, firstLevelMenu.Id);
                    if (secondLevelMenus != null && secondLevelMenus.Count > 0)
                    {
                        foreach (var secondLevelMenu in secondLevelMenus)
                        {
                            menuModel.Children.Add(new MenuModel()
                            {
                                Id = secondLevelMenu.Id,
                                Name = secondLevelMenu.Name,
                                Url = secondLevelMenu.Url
                            });
                        }
                        menumodel.Add(menuModel);
                    }
                }
            }
            return Success(menumodel);
        }
    }
}