using AMC.CORE.Enumerations;

namespace AMC.CORE.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public bool IsRegistered { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; }
    }
}
