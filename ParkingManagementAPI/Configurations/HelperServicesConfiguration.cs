using ParkingManagementAPI.Services.Helpers;

namespace ParkingManagementAPI.Configurations
{
    public static class HelperServicesConfiguration
    {
        public static IServiceCollection AddHelperServices(this IServiceCollection services)
        {
            services.AddScoped<ChargeCalculationHelper>();
            services.AddScoped<RequestVerificationHelper>();

            return services;
        }
    }
}
