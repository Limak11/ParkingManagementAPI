using ParkingManagementAPI.Models.Database;
using ParkingManagementAPI.Services;


namespace ParkingManagementAPI.Tests
{
    public class DatabaseServiceTest
    {
        private readonly DatabaseService _databaseService;

        public DatabaseServiceTest()
        {
            _databaseService = Initializer.InitDatabaseService();
        }

        //TODO: split into several tests
        [Fact]
        public async Task DatabaseService_Logic_Correct()
        {
            for(int i=1; i<=5; i++)
            {
                await _databaseService.AddParkingSpotAsync(i);
            }

            var availableSpots = await _databaseService.GetAvailableSpotsAsync();

            Assert.Equal(5, availableSpots.Count);

            var occupiedSpots = await _databaseService.GetOccupiedSpotsAsync();

            Assert.Empty(occupiedSpots);

            var perMinuteCharge = 1;
            await _databaseService.AddChargeRateAsync(1, perMinuteCharge, 1);

            var chargeRate = await _databaseService.GetChargeRateByVehicleTypeAsync(1);

            Assert.NotNull(chargeRate);

            Assert.Equal(perMinuteCharge, chargeRate.ChargePerMinute);

            var allocation = new ParkingSpotAllocation { VehicleRegistration = "DW12345", ChargeRate = chargeRate, ParkingSpot = availableSpots.First() };

            await _databaseService.SaveAllocationAsync(allocation);

            availableSpots = await _databaseService.GetAvailableSpotsAsync();

            Assert.Equal(4, availableSpots.Count);

            var retrievedAllocation = await _databaseService.GetCurrentAllocationByVehicleRegistrationAsync("DW12345");

            Assert.NotNull(retrievedAllocation);

            retrievedAllocation.EndDateTime = DateTime.Now;

            await _databaseService.CloseAllocationAsync(retrievedAllocation);

            availableSpots = await _databaseService.GetAvailableSpotsAsync();

            Assert.Equal(5, availableSpots.Count);

            retrievedAllocation = await _databaseService.GetCurrentAllocationByVehicleRegistrationAsync("DW12345");

            Assert.Null(retrievedAllocation);
        }
    }
}
