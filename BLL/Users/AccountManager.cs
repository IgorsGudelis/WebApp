using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTO;
using DAL.Users;

namespace BLL.Users
{
    public class AccountManager:IAccountManager
    {
        private readonly IUserAuthRepository _userAuthRepository;

        public AccountManager(IUserAuthRepository repository)
        {
            _userAuthRepository = repository;
        }

        public UserAuth GetUserAuth(UserAuth user)
        {          
            var userDto = _userAuthRepository.GetUser(new UserAuthDto {Name = user.Name, Password = user.Password});

            if (userDto == null) { return null;}

            return new UserAuth
                {
                    Name = userDto.Name,
                    Password = userDto.Password
                };
        }

        public UserAuth FindUserAuth(string name)
        {
            var userDto = _userAuthRepository.FindUser(name);

            if (userDto == null) { return null; }

            return new UserAuth
            {
                Name = userDto.Name,
                Password = userDto.Password
            };
        }

        public void AddUserAuth(UserAuth user)
        {
            _userAuthRepository.AddUserAuth(new UserAuthDto{Name = user.Name, Password = user.Password});
        }
    }
}
