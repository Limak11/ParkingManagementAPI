using ParkingManagementAPI.Factories;

namespace ParkingManagementAPI.Configurations
{
    public static class FactoriesConfiguration
    {
        public static IServiceCollection AddFactoryServices(this IServiceCollection services)
        {
            services.AddScoped<IHttpResponseFactory, HttpResponseFactory>();
            services.AddScoped<IDatabaseEntryFactory, DatabaseEntryFactory>();

            return services;
        }
    }
}
