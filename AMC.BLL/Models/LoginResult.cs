using System.Security.Claims;

namespace AMC.BLL.Models
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public ClaimsPrincipal Principal { get; set; }
    }
}
