using System.Collections.Generic;

namespace BLL.Users
{
    public interface IUsersManager
    {
        List<User> GetAllUsers();
        List<User> GetUsers(string find, int currentPage, out int countPages);
        void AddUser(string firstName, string lastName, string email);
        void DeleteUser(int idParam);
        User EditUserQuery(int idParam);
        void EditUser(int idParam, string firstName, string lastName, string eMail);
    }
}
