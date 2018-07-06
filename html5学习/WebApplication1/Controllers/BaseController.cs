using Redis;
using SqlHelpCore.Model.User;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using helpClass.Utils;
using SqlHelpCore.Model.admin;

namespace WebApplication1.Controllers
{
    public class BaseController : Controller
    {
        protected Administrator CurrentUser
        {
            get
            {
                RedisSession.SetExpire("ADMINUSER", 30);
                return RedisSession.Get<Administrator>("ADMINUSER");
            }
            set { RedisSession.Add("ADMINUSER", value); }
        }

        public RedisSession RedisSession => new RedisSession(System.Web.HttpContext.Current);

        //成功执行返回
        protected string Success()
        {
            return new { code = 1 }.ToJson();
        }

        //成功执行返回
        protected string Success(string message)
        {
            return new { code = 1, msg = message }.ToJson();
        }

        //成功执行返回
        protected string Success(object data)
        {
            return new { code = 1, data = data }.ToJson();
        }

        //成功执行返回
        protected string Success(int total, object list)
        {
            return new { code = 1, total = total, rows = list }.ToJson();
        }

        //失败执行返回
        protected string Error(string message)
        {
            return new { code = 0, msg = message }.ToJson();
        }

        protected string Error(int code, string msg)
        {
            return new { code = code, msg = msg }.ToJson();
        }


        //获取模型状态第一个错误消息
        protected string GetFirstErrorMessage()
        {
            return GetAllErrorMessage().First();
        }

        //获取模型状态所有的错误消息
        protected IEnumerable<string> GetAllErrorMessage()
        {
            return ModelState.Values.SelectMany(m => m.Errors).Select(m => m.ErrorMessage);
        }
    }
}