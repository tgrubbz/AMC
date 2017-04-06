using AMC.CORE.Enumerations;

namespace AMC.WEB.ViewModels.User
{
    public class UserViewModel
    {
        public string Username { get; set; }
        public UserRole Role { get; set; }

        public string RoleName
        {
            get
            {
                return Role.ToFriendlyString();
            }
        }

        public UserViewModel()
        {
        }

        public UserViewModel(CORE.Models.User user)
        {
            Username = user.Username;
            Role = user.Role;
        }
    }
}
