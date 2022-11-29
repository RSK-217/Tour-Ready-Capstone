using Microsoft.AspNetCore.Mvc;
using Tour_Ready_Capstone.Models;

namespace Tour_Ready_Capstone.Interfaces
{
    public interface IUser
    {
        public List<User> GetAllUsers();
        public User GetUserById(int id);
        public User CreateUser(User user);
        public void UpdateUser(User user);
    }
}
