using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Users
{
    public interface IAccountManager
    {
        UserAuth GetUserAuth(UserAuth user);

        UserAuth FindUserAuth(string name);

        void AddUserAuth(UserAuth user);
    }
}
