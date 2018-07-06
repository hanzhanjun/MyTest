using SqlHelpCore.Model.admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlHelpCore.Dto.User;
using System.Linq.Expressions;
using helpClass.Utils;
using SqlHelpCore.Model.User;

namespace SqlHelpCore.Application.Interface
{
    public interface IAdministratorService
    {
        /// <summary>
        /// 是否有权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        bool IsAuthorizePass(int id, string code);
       
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Administrator Login(string username, string password);

        /// <summary>
        /// 获取管理员后台一级菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<AdminMenu> GetAdminFirstMenu(int adminId);

        /// <summary>
        /// 二级菜单
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="parentMenuId"></param>
        /// <returns></returns>
        List<AdminMenu> GetAdministratorsecondMenu(int adminId, string parentMenuId);

        bool AddAdmin(User user);

        /// <summary>
        /// 通过目录获取权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        List<AdminAction> GetActionByMenuId(int id, string menuId);

        /// <summary>
        /// 获取user
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        PagedResult<UserDto> GetList(Expression<Func<UserDto, bool>> expression, int page, int rows);
        bool EditAdmin(User user);
        User GetUser(int id);

        List<UserDto> Excel(Expression<Func<UserDto, bool>> expression);
    }
}
