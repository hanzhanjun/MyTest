using helpClass.Utils;
using SqlHelpCore.Dto.User;
using SqlHelpCore.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace WebApplication1.Models.HX01
{
    public class ChenkPageModelcs
    {
        public string UserName { get; set; }

        public string Mobile { get; set; }

        public DateTime startTime { get; set; }

        public DateTime endTime { get; set; }

        public Expression<Func<UserDto, bool>> build()
        {
            Expression<Func<UserDto, bool>> exp = s => true;

            if (!this.Mobile.IsNullOrEmpty())
            {
                exp = exp.And(s => s.Moblie == this.Mobile);
            }

            if (!this.UserName.IsNullOrEmpty())
            {
                exp = exp.And(s => s.UserName == this.UserName);
            }

            if (this.startTime != DateTime.MinValue)
            {
                endTime = endTime == DateTime.MinValue ? DateTime.Now : endTime;
                var end = endTime.AddDays(1).AddSeconds(-1);
                exp = exp.And(s => s.CreateTime > this.startTime && s.CreateTime < end);
            }

            return exp;
        }
    }
}