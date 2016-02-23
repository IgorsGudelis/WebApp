using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTO;

namespace DAL.Users
{
    public interface IUserAuthRepository
    {
        IEnumerable<UserAuthDto> UsersAuthDto { get; }

        UserAuthDto GetUser(UserAuthDto user);

        UserAuthDto FindUser(string name);

        void AddUserAuth(UserAuthDto user);
    }
}
