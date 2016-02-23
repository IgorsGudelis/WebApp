using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTO;

namespace DAL.Users
{
    public class UserAuthRepository: IUserAuthRepository
    {
        private readonly Myblog _contexMyblog = new Myblog();

        public IEnumerable<UserAuthDto> UsersAuthDto { get { return _contexMyblog.UsersAuth; } }
        public UserAuthDto GetUser(UserAuthDto user)
        {
            return UsersAuthDto.FirstOrDefault(u => u.Name == user.Name && u.Password == user.Password);
        }

        public UserAuthDto FindUser(string name)
        {
            return UsersAuthDto.FirstOrDefault(u => u.Name == name);
        }

        public void AddUserAuth(UserAuthDto user)
        {
            _contexMyblog.UsersAuth.Add(user);
            _contexMyblog.SaveChanges();
        }
    }
}
