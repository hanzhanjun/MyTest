using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlHelpCore.Model.admin
{
    public class Role
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public int? Createon { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 是否允许删除
        /// </summary>
        public int IsDelete { get; set; }
    }
}
