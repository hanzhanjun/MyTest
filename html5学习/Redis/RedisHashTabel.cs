using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis
{
    /// <summary>
    /// 用户session关系
    /// </summary>
    public class RedisHashTabel
    {

        public string SessionId { get; set; }
        public int UserId { get; set; }
    }
}
