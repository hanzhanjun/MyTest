using SqlHelpCore.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlHelpCore.Application.Interface
{
    public interface IUserService
    {
        User Login(string userName, string password);
    }
}
