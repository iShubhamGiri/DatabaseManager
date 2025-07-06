using System.Collections.Generic;
using DatabaseManager.Models;

namespace DatabaseManager.Interfaces
{
    public interface IUserRepository
    {
        void InsertUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
        User GetUserById(int id);
        List<User> GetAllUsers();
    }
}
