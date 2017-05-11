using AMC.BLL.Interfaces;
using AMC.BLL.Models;
using AMC.CORE.Enumerations;
using AMC.CORE.Models;
using AMC.DAL.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;

namespace AMC.BLL.Managers
{
    public class UserManager : IUserManager
    {
        IUserRepository _userRepo;

        public UserManager(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public int Setup(User user)
        {
            user.IsSetup = true;
            // TODO: Email user that they are setup
            return _userRepo.Update(user);
        }

        public DataTableResult<User> GetUsersTable(DataTableRequest request)
        {
            return _userRepo.GetTable(request);
        }

        public LoginResult Login(string username, string password)
        {
            LoginResult result = new LoginResult();
            User user = _userRepo.Read(username);

            // Check that user exists
            if (user != null)
            {
                // Check that user is registered
                if (user.IsRegistered)
                {
                    // Check that user is setup
                    if (user.IsSetup)
                    {
                        // Verify the entered password
                        if (PasswordHasher.VerifyHashedPassword(user.PasswordHash, password))
                        {
                            // Create the identity
                            List<Claim> claims = new List<Claim>();
                            claims.Add(new Claim("Username", user.Username));
                            claims.Add(new Claim("Role", user.Role.ToClaimString()));

                            ClaimsIdentity identity = new ClaimsIdentity(claims, "password");
                            result.Principal = new ClaimsPrincipal(identity);

                            result.Success = true;
                        }
                        else
                        {
                            result.Error = "Incorrect password";
                        }
                    }
                    else
                    {
                        result.Error = "User must be setup by an administator";
                    }
                }
                else
                {
                    result.Error = "Username must be registered";
                }
            }
            else
            {
                result.Error = "Username does not exist";
            }

            return result;
        }

        public User Read(string username)
        {
            return _userRepo.Read(username);
        }

        /// <summary>
        /// Registers a User (new or predefined)
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public RegisterResult Register(string username, string password)
        {
            RegisterResult result = new RegisterResult();
            User user = _userRepo.Read(username);
            
            if (user == null)
            {
                // Create new user
                user = new User();
                user.Username = username;
                user.PasswordHash = PasswordHasher.HashPassword(password);

                // Mark as registered
                user.IsRegistered = true;

                if (Create(user) > 0)
                {
                    result.Success = true;
                }
            }
            else if (!user.IsRegistered)
            {
                // Created but not registered
                user.PasswordHash = PasswordHasher.HashPassword(password);

                // Mark as registered
                user.IsRegistered = true;

                if (Update(user) > 0)
                {
                    result.Success = true;
                }
            }

            return result;
        }

        public int Update(User user)
        {
            return _userRepo.Update(user);
        }

        public int Create(User user)
        {
            return _userRepo.Create(user);
        }

        public int Delete(int id)
        {
            return _userRepo.Delete(id);
        }
    }
}
