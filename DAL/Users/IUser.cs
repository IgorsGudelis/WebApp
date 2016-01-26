using System.Collections.Generic;
using DTO;

namespace DAL.Users
{
    public interface IUser
    {
        List<UserDTO> GetAllUsers(string user = null);

        List<UserDTO> GetUsers(string user);

        void AddUserDto(UserDTO addUserDto);

        void DeleteUser(int idParam);

        UserDTO EditUserQuery(int idParam);

        void EditUser(int idParam, string firstName, string lastName, string eMail);
    }
}
