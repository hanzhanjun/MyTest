using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlHelpCore.Model.User
{
    public class User
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string RealName { get; set; }

        public string Moblie { get; set; }

        public string Email { get; set; }

        public string PassWard { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime Birthday { get; set; }

        public string Qq { get; set; }

        public int Sex { get; set; }
    }
}
