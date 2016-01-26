using System.Collections.Generic;
using System.Linq;
using DAL.Users;
using DTO;

namespace BLL.Users
{
    public class UsersManager: IUsersManager
    {
        private readonly IUser _user;
        
        public UsersManager(IUser user) {_user = user;}

        public List<User> GetAllUsers()
        {
            var allUsers = _user.GetAllUsers();
           
            return allUsers.Select(userDto => new User()
            {
                ID = userDto.ID,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email
            }).ToList();
        }

        public List<User> GetUsers(string find, int currentPage, out int countPages)
        {
            const int pageSize = 4;

            var countItems = (find != null) ? _user.GetUsers(find).Count() : _user.GetAllUsers().Count();

            if (countItems % pageSize != 0)
            {
                countPages = countItems/pageSize + 1;
            }
            else
            {
                countPages = countItems/pageSize;
            }

            var allUsers = _user.GetUsers(find);
            allUsers = allUsers.Skip((currentPage - 1)*pageSize).Take(pageSize).ToList();

            return allUsers.Select(userDto => new User()
             {
                 ID = userDto.ID,
                 FirstName = userDto.FirstName,
                 LastName = userDto.LastName,
                 Email = userDto.Email
             }).ToList();
        }

        public void AddUser(string firstName, string lastName, string email)
        {
            var addUserDto = new UserDTO()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };

            _user.AddUserDto(addUserDto);
        }

        public void DeleteUser(int idParam)
        {
            _user.DeleteUser(idParam);
        }

        public User EditUserQuery(int idParam)
        {
            var userDto = _user.EditUserQuery(idParam);

            var user = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                ID = idParam
            };

            return user;
        }

        public void EditUser(int idParam, string firstName, string lastName, string eMail)
        {
            _user.EditUser(idParam, firstName, lastName, eMail);
        }
    }
}
