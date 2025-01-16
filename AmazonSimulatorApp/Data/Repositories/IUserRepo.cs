using AmazonSimulatorApp.Data;


namespace AmazonSimulatorApp.Repositories
{
    public interface IUserRepo
    {
        User AddUser(User user);
        void DeleteUser(User user);
        IEnumerable<User> GetAllUsers();
        User GetUserByEmail(string email);
        User GetUserById(int id);
        User UpdateUser(User user);
        User GetUserByName(string userName);
        IEnumerable<User> GetUserByRole(string roleName);
        bool IsValidRole(string roleName);
        bool EmailExists(string email);
    }
}