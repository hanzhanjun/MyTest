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
    public class AccountController : BaseController
    {

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var administratorservice = Container.Instance.Resolve<IAdministratorService>();
            try
            {
                var admin = administratorservice.Login(username, password);
                if (admin != null)
                {
                    CurrentUser = admin;
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (HelpException ex)
            {
                ViewBag.Content = ex.Message;
            }
            return View();
        }  
    }
}