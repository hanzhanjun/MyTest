using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.home
{
    public class MenuModel
    {
        public MenuModel()
        {
            Children = new List<MenuModel>();
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public string Url { get; set; }
      
        /// <summary>
        /// 子菜单
        /// </summary>
        public List<MenuModel> Children { get; set; }
    }
}