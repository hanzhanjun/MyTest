using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlHelpCore.Utils
{
    public class GlobalParams
    {
        /// <summary>
        /// 每页显示记录数
        /// </summary>
        public static readonly int DEFAULT_PAGESIZE = 15;

        /// <summary>
        /// Excel导出最大导出数量
        /// </summary>
        public static readonly int DEFAULT_EXCEL_PAGESIZE = 100000;
        /// <summary>
        /// 详细内容区域下拉列表默认值
        /// </summary>
        public static readonly string DEFAULT_QUERY_SELECT_ID = "-999";
        /// <summary>
        /// 详细内容区域下拉列表默认显示
        /// </summary>
        public static readonly string DEFAULT_QUERY_SELECT_NAME = "全部";

        /// <summary>
        /// 查询区域下拉列表默认值
        /// </summary>
        public static readonly string DEFAULT_DETAIL_SELECT_ID = "-999";
        /// <summary>
        /// 查询区域下拉列表默认显示
        /// </summary>
        public static readonly string DEFAULT_DETAIL_SELECT_NAME = "--请选择--";
        /// <summary>
        /// 最大时间
        /// </summary>
        public static readonly string IL_SYS_MAXDATETIME = "2099-12-31 00:00:00";
        /// <summary>
        /// 最小时间
        /// </summary>
        public static readonly string IL_SYS_MINDATETIME = "1900-01-01 00:00:00";

        /// <summary>
        /// 用户图像上传大小（500KB）
        /// </summary>
        public static readonly int USER_IMAGE_UPLOAD_SIZE = 512000;

        /// <summary>
        /// web开仓
        /// </summary>
        public static readonly string WEB_OPEN_POSITION = "Web开仓";

        /// <summary>
        /// web平仓
        /// </summary>
        public static readonly string WEB_CLOSE_POSITION = "Web平仓";

        /// <summary>
        /// App开仓
        /// </summary>
        public static readonly string APP_OPEN_POSITION = "App开仓";

        /// <summary>
        /// App平仓
        /// </summary>
        public static readonly string APP_CLOSE_POSITION = "App平仓";

        /// <summary>
        /// 最大关注人数
        /// </summary>
        public static readonly int MAX_FOLLOW_COUNT = 10;
    }
}
