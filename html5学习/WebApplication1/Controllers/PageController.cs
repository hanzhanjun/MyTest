using Autofac;
using SqlHelpCore;
using SqlHelpCore.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class PageController : BaseController
    {
        // GET: Page
        public JsonResult SetControlsDisplay(string id)
        {
            var systemService = Container.Instance.Resolve<IAdministratorService>();
            var list = systemService.GetActionByMenuId(CurrentUser.RoleId, id);

            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach (var item in list)
            {
                dict.Add(item.ActionKey, 1);
            }
            return Json(dict);
        }
    }
}