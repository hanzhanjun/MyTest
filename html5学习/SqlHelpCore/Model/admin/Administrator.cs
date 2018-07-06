using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlHelpCore.Model.admin
{
    public class Administrator
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        //角色Id
        public int RoleId { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
