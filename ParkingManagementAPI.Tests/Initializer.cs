using Microsoft.EntityFrameworkCore;
using ParkingManagementAPI.Context;
using ParkingManagementAPI.Services;

namespace ParkingManagementAPI.Tests
{
    internal static class Initializer
    {
        internal static DatabaseService InitDatabaseService() => new DatabaseService(InitInMemoryDatabase());

        private static ParkingContext InitInMemoryDatabase()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ParkingContext>();
            optionsBuilder.UseInMemoryDatabase("TestDb");

            return new ParkingContext(optionsBuilder.Options);
        }
    }
}
