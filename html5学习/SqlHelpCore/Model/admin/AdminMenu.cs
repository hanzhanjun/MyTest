using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlHelpCore.Model.admin
{
    public class AdminMenu
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 父节点编号
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// URL链接
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public int IsVisible { get; set; }
    }
}
