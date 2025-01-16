using AmazonSimulatorApp.Data;
using AmazonSimulatorApp.Data.DTOs;
using AmazonSimulatorApp.Repositories;
using AmazonSimulatorApp.Services;


namespace AmazonSimulatorApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;

        public UserService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public void AddUser(User user)
        {
            _userRepo.AddUser(user);
        }
        public void DeleteUser(int uid)
        {
            var user = _userRepo.GetUserById(uid);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {uid} not found.");

            _userRepo.DeleteUser(user);
        }
        public IEnumerable<User> GetAllUsers()
        {
            return _userRepo.GetAllUsers();
        }
   
        public User GetUserById(int uid)
        {
            var user = _userRepo.GetUserById(uid);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {uid} not found.");
            return user;
        }
        public void UpdateUser(User user)
        {
            var existingUser = _userRepo.GetUserById(user.ID);
            if (existingUser == null)
                throw new KeyNotFoundException($"User with ID {user.ID} not found.");

            _userRepo.UpdateUser(user);
        }
        public User GetUserByName(string userName)
        {
            var user = _userRepo.GetUserByName(userName);
            if (user == null)
                throw new KeyNotFoundException($"User with Name {userName} not found.");
            return user;
        }

        public UserOutputDTO GetUserData(string? userName, int? uid)
        {
            User user = null;

            // Validate that at least one parameter is provided
            if (string.IsNullOrWhiteSpace(userName) && !uid.HasValue)
                throw new ArgumentException("Either username or user ID must be provided.");

            // Retrieve user based on username 
            if (!string.IsNullOrEmpty(userName))
                user = GetUserByName(userName);

            // Retrieve user based on UID
            if (uid.HasValue)
                user = GetUserById(uid.Value);


            if (user == null)
                throw new KeyNotFoundException($"User not found.");

            var outputData = new UserOutputDTO
            {
                ID = user.ID,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                
            };

            return (outputData);
        }

        public IEnumerable<UserOutputDTO> GetUserByRole(string roleName)
        {
            var users = _userRepo.GetUserByRole(roleName);
            if (users == null)
                throw new KeyNotFoundException($"No Users found");

            List<UserOutputDTO> output = new List<UserOutputDTO>();

            foreach (var user in users)
            {
                // Transform active users into DTOs
                output.Add(new UserOutputDTO
                {
                    ID = user.ID,
                    Name = user.Name,
                    Email = user.Email,
                    Role = user.Role,
               
                });
            }
            return (output);
        }
        public bool EmailExists(string email)
        {
            return _userRepo.EmailExists(email);
        }
    }

}

