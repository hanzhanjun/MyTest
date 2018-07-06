using SqlHelpCore.Application.Interface;
using SqlHelpCore.Model.User;
using System.Linq;

namespace SqlHelpCore.Application.Impl
{

    public class UserService : IUserService
    {
        public User Login(string userName, string password)
        {
            using (HelpDbContext db = new HelpDbContext())
            {
                return db.Users.FirstOrDefault(s => s.UserName == userName && s.PassWard == password);
            }
        }
    }
}
