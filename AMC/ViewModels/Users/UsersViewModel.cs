using AMC.CORE.Enumerations;

namespace AMC.WEB.ViewModels.Users
{
    public class UsersViewModel
    {
        public string Username { get; set; }
        public UserRole Role { get; set; }
        public bool IsRegistered { get; set; }
        public bool IsSetup { get; set; }

        public string RoleName
        {
            get
            {
                return Role.ToFriendlyString();
            }
        }

        public UsersViewModel()
        {
        }

        public UsersViewModel(CORE.Models.User user)
        {
            Username = user.Username;
            Role = user.Role;
            IsRegistered = user.IsRegistered;
            IsSetup = user.IsSetup;
        }
    }
}
