using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlHelpCore.Model.admin
{
    public class RoleAction
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 功能Id
        /// </summary>
        public string ActionId { get; set; }
    }
}
