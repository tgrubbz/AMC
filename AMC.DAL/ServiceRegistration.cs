using AMC.DAL.Interfaces;
using AMC.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;

namespace AMC.DAL
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddDAL(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IDbConnection>(new SqlConnection(connectionString));
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
