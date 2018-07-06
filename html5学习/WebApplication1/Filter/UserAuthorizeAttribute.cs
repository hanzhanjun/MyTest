using Autofac;
using Redis;
using SqlHelpCore;
using SqlHelpCore.Application.Interface;
using SqlHelpCore.Model.admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Filter
{
    public class UserAuthorizeAttribute: AuthorizeAttribute
    {
        /// <summary>
        ///权限代号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            RedisSession redisSession = new RedisSession(HttpContext.Current);
            var user = redisSession.Get<Administrator>("ADMINUSER");
            if (user != null)
            {
                if (Code != null)
                {
                    var adminService = Container.Instance.Resolve<IAdministratorService>();
                    bool pass = adminService.IsAuthorizePass(user.RoleId, Code);
                    return pass;
                }
                return true;
            }
            return false;
        }


        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (IsAjax())
            {
                filterContext.HttpContext.Response.StatusCode = 401;
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                filterContext.Result = new JsonResult
                {
                    Data = new { msg = "您的登录已失效，请重新登录！", url = "/account/login" },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                filterContext.Result = new RedirectResult("/account/login");
            }
        }

        private bool IsAjax()
        {
            return HttpContext.Current.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }
    }
}