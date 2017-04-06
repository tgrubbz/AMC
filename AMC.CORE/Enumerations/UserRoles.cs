using System;

namespace AMC.CORE.Enumerations
{
    public enum UserRole
    {
        Client,
        Contractor,
        Notary,
        Admin
    }

    public static class UserRoleExtensions
    {
        public static string ToClaimString(this UserRole role)
        {
            return ((int)role).ToString();
        }

        public static UserRole FromClaimString(this string role)
        {
            return (UserRole)Convert.ToInt32(role);
        }

        public static string ToFriendlyString(this UserRole role)
        {
            return role.ToString();
        }
    }
}
