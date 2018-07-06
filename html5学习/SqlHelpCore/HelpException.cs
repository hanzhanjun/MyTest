using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlHelpCore
{
    public class HelpException : ApplicationException
    {
        /// <summary>
        /// 弹出消息
        /// </summary>
        /// <param name="message"></param>
        public HelpException(string message) : base(message)
        { }
    }
}
