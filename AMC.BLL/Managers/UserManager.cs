using AMC.BLL.Interfaces;
using AMC.BLL.Models;
using AMC.CORE.Enumerations;
using AMC.CORE.Models;
using AMC.DAL.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;
using System;

namespace AMC.BLL.Managers
{
    public class UserManager : IUserManager
    {
        IUserRepository _userRepo;

        public UserManager(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public int Create(string username)
        {
            return _userRepo.Create(new User() { Username = username });
        }

        public TableResult<User> GetUsersTable()
        {
            return _userRepo.GetTable();
        }

        public LoginResult Login(string username, string password)
        {
            LoginResult result = new LoginResult();
            User user = _userRepo.Read(username);

            if (user != null)
            {
                if (user.IsRegistered)
                {
                    result.IsRegistered = true;
                    if (PasswordHasher.VerifyHashedPassword(user.PasswordHash, password))
                    {
                        List<Claim> claims = new List<Claim>
                        {
                            new Claim("Username", user.Username),
                            new Claim("Role", user.Role.ToClaimString())
                        };

                        ClaimsIdentity identity = new ClaimsIdentity(claims, "password");
                        result.Principal = new ClaimsPrincipal(identity);

                        result.Success = true;
                    }
                }
            }

            return result;
        }

        public User Read(string username)
        {
            return _userRepo.Read(username);
        }

        public RegisterResult Register(string username, string password)
        {
            RegisterResult result = new RegisterResult();
            User user = _userRepo.Read(username);

            if (user != null)
            {
                // Check to see if the user is already registered
                if (!user.IsRegistered)
                {
                    user.PasswordHash = PasswordHasher.HashPassword(password);
                    user.IsRegistered = true;

                    if (Update(user) > 0)
                    {
                        result.Success = true;
                    }
                }
            }

            return result;
        }

        public int Update(User user)
        {
            return _userRepo.Update(user);
        }
    }
}
