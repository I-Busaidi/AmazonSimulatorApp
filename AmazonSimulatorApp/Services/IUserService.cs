using AmazonSimulatorApp.Data;
using AmazonSimulatorApp.Data.DTOs;


namespace AmazonSimulatorApp.Services
{
    public interface IUserService
    {
        void AddUser(User user);
        void DeleteUser(int uid);
        IEnumerable<User> GetAllUsers();
        User GetUserById(int uid);
        void UpdateUser(User user);
        bool EmailExists(string email);
        IEnumerable<UserOutputDTO> GetUserByRole(string roleName);
        UserOutputDTO GetUserData(string? userName, int? uid);
    }
}