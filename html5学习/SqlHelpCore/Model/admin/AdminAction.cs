using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlHelpCore.Model.admin
{
    public class AdminAction
    {
        /// <summary>
        /// 权限操作ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 键值
        /// </summary>
        public string ActionKey { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 菜单ID
        /// </summary>
        public string MenuId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}
