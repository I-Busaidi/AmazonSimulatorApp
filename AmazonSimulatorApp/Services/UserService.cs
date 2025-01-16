using AmazonSimulatorApp.Data;
using AmazonSimulatorApp.Data.DTOs;
using AmazonSimulatorApp.Repositories;



namespace AmazonSimulatorApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly IConfiguration _configuration;
   

        public UserService(IUserRepo userRepo, IConfiguration configuration)
        {
            _userRepo = userRepo;
            _configuration = configuration;
          

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
        public void AddAdmin(UserInputDTO InputUser)
        {
           

            //check if there is any active supper admin
            var existingAdmins = _userRepo.GetUserByRole(InputUser.Role);
            if (existingAdmins != null && existingAdmins.Any(u => u.IsActive))
            {
                throw new InvalidOperationException("Only one active super admin is allowed in the system.");
            }
            else
            {
                // Default password and email generation
                String defaultPassword = "Admin1234";
                string sanitizedUserName = InputUser.Name.Replace(" ", "");
                string generatedEmail = $"{sanitizedUserName}@CodelineHospital.com";
                


                // Create new  admin user
                var newAdmin = new User
                {
                    Name = InputUser.Name,
                    Email = generatedEmail,
                    
                    Role = InputUser.Role,
                    IsActive = true
                };
                // Email subject and body
                string subject = "Hospital System Signing In";
                string body = $"Dear {InputUser.Name},\n\nYour  Admin account has been created successfully.\n\nYour default password is: " +
               $"{defaultPassword}\nPlease change your password after logging in.\n\nBest Regards,\nYour System Team";

                // Add the new super admin to the repository
                _userRepo.AddUser(newAdmin);
                // Send email
                //_email.SendEmailAsync("hospitalproject2025@outlook.com", subject, body);
            }

        }

        public async Task AddClientAndSeller(UserInputDTO InputUser)
        {
            

            const string defaultPassword = "Pass1234";
            // Sanitize the UserName to remove spaces
            string sanitizedUserName = InputUser.Name.Replace(" ", "");

            Random random = new Random();
            int randomNumber;
            string generatedEmail;

            // Ensure unique email generation
            do
            {
                randomNumber = random.Next(1000, 9999);
                generatedEmail = $"{sanitizedUserName}{randomNumber}@CodelineHospital.com";
            } while (_userRepo.EmailExists(generatedEmail));

         

            var newStaff = new User
            {
                Name = InputUser.Name,
                Email = generatedEmail,
           
              
                Role = InputUser.Role,
                IsActive = true
            };

            // Email subject and body
            string subject = "Hospital System";
            string body = $"Dear {InputUser.Name},\n\nYour account has been created successfully for Amazon App.\n\n" +
                          $"Email: {generatedEmail}\nYour default password is: {defaultPassword}\n" +
                          $"Please change your password after logging in.\n\nBest Regards,\nYour  Admin";

            // Send email asynchronously
            //await _email.SendEmailAsync("hospitalproject2025@outlook.com", subject, body);

            // Add user to the database
            _userRepo.AddUser(newStaff);
        }


    }

}

