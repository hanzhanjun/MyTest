using SqlHelpCore.Application.Interface;
using SqlHelpCore.Model.admin;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using helpClass.Utils;
using SqlHelpCore.Dto.User;
using System.Linq.Expressions;
using SqlHelpCore.Model.User;

namespace SqlHelpCore.Application.Impl
{
    public class AdministratorService : IAdministratorService
    {
        /// <summary>
        /// 是否有权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool IsAuthorizePass(int id, string code)
        {
            using (HelpDbContext ctx = new HelpDbContext())
            {
                string sql = string.Format(@"SELECT  [actionid] as [Id]
                                                ,[actionkey] as [ActionKey]
                                                ,[actionname] as [ActionName]
                                                ,[menuid] as [MenuId]
                                                ,[sort] as [Sort] from admin_action
                                            where actionid = 
                                            (
                                               select actionid from admin_roleaction
                                               where roleid = {0} and actionid = '{1}'
                                             )", id, code);
                var action = ctx.Database.SqlQuery<Model.admin.AdminAction>(sql).FirstOrDefault();

                return action == null ? false : true;
            }
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Administrator Login(string username, string password)
        {
            using (HelpDbContext db = new HelpDbContext())
            {
                return db.Administrators.FirstOrDefault(s => s.UserName == username && s.Password == password);
            }
        }


        /// <summary>
        /// 获取管理员后台一级菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<AdminMenu> GetAdminFirstMenu(int adminId)
        {
            return GetAdministratorsecondMenu(adminId, "");
        }

        /// <summary>
        /// 二级菜单
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="parentMenuId"></param>
        /// <returns></returns>
        public List<AdminMenu> GetAdministratorsecondMenu(int adminId, string parentMenuId)
        {
            using (HelpDbContext ctx = new HelpDbContext())
            {
                var sqlParm1 = new SqlParameter
                {
                    ParameterName = "@UserID",
                    Value = adminId,
                    Direction = System.Data.ParameterDirection.Input
                };

                var sqlParm2 = new SqlParameter
                {
                    ParameterName = "@ParentMenuID",
                    Value = parentMenuId,
                    Direction = System.Data.ParameterDirection.Input
                };

                return ctx.Database.SqlQuery<AdminMenu>("exec UserMenu @UserID,@ParentMenuID", sqlParm1, sqlParm2).AsQueryable().ToListOrDefault();
            }
        }

        /// <summary>
        /// 通过目录获取权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<AdminAction> GetActionByMenuId(int id, string menuId)
        {
            using (HelpDbContext ctx = new HelpDbContext())
            {
                string sql = string.Format(@"SELECT  [actionid] as [Id]
                                                ,[actionkey] as [ActionKey]
                                                ,[actionname] as [ActionName]
                                                ,[menuid] as [MenuId]
                                                ,[sort] as [Sort] from admin_action
                                            where menuid='{0}' and actionid in 
                                            (
                                               select actionid from admin_roleaction
                                               where roleid = {1}
                                             )", menuId, id);
                var actions = ctx.Database.SqlQuery<AdminAction>(sql).AsQueryable();

                return actions.ToListOrDefault();
            }
        }

        /// <summary>
        /// 获取user
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public PagedResult<UserDto> GetList(Expression<Func<UserDto, bool>> expression, int page, int rows)
        {
            using (HelpDbContext db = new HelpDbContext())
            {
                var qry = from u in db.Users
                          select new UserDto
                          {
                              Id = u.Id,
                              UserName = u.UserName,
                              Moblie = u.Moblie,
                              RealName = u.RealName,
                              PassWard = u.PassWard,
                              Email = u.Email,
                              CreateTime = u.CreateTime,
                              Birthday = u.Birthday,
                              Qq = u.Qq,
                              Sex = u.Sex == 1 ? "男" :"女"
                          };
                qry = qry.Where(expression);
                return new PagedResult<UserDto>()
                {
                   PageNumber = page,
                   PageSize = rows,
                   Data = qry.OrderByDescending(s => s.CreateTime).Skip((page - 1) * rows).Take(rows).ToListOrDefault(),
                   TotalRecords = qry.Count()
                };
            }
        }

        public bool AddAdmin(User user)
        {
            using (HelpDbContext db = new HelpDbContext())
            {
                var model = db.Users.FirstOrDefault(s => s.UserName == user.UserName);
                if (model != null)
                {
                    return false;
                }
                db.Users.Add(user);
                return db.SaveChanges() > 0;
            }
        }

        public User GetUser(int id)
        {
            using (HelpDbContext db = new HelpDbContext())
            {
                return db.Users.FirstOrDefault(s => s.Id == id);
            }
        }


        public bool EditAdmin(User user)
        {
            using (HelpDbContext db = new HelpDbContext())
            {
                var model = db.Users.FirstOrDefault(s => s.Id == user.Id);
                model.UserName = user.UserName;
                model.RealName = user.RealName;
                model.Sex = user.Sex;
                model.Moblie = user.Moblie;
                model.Qq = user.Qq;
                model.Email = user.Email;
                model.Birthday = user.Birthday;
                return db.SaveChanges() > 0;
            }
            
        }

        public List<UserDto> Excel(Expression<Func<UserDto, bool>> expression)
        {
            using (HelpDbContext db = new HelpDbContext())
            {
                var qry = from u in db.Users
                          select new UserDto
                          {
                              Id = u.Id,
                              UserName = u.UserName,
                              Moblie = u.Moblie,
                              RealName = u.RealName,
                              PassWard = u.PassWard,
                              Email = u.Email,
                              CreateTime = u.CreateTime,
                              Birthday = u.Birthday,
                              Qq = u.Qq,
                              Sex = u.Sex == 1 ? "男" : "女"
                          };
                return qry.Where(expression).ToList();               
            }
        }
    }
}
