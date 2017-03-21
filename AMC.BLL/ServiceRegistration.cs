using AMC.BLL.Interfaces;
using AMC.BLL.Managers;
using AMC.DAL;
using Microsoft.Extensions.DependencyInjection;

namespace AMC.BLL
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddBLL(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IUserManager, UserManager>();
            services.AddDAL(connectionString);
            return services;
        }
    }
}
