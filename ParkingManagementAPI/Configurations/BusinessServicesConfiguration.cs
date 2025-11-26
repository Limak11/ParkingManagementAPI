using ParkingManagementAPI.Services;

namespace ParkingManagementAPI.Configurations
{
    public static class BusinessServicesConfiguration
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<DatabaseService>();
            services.AddScoped<ParkingService>();

            return services;
        }
    }
}
