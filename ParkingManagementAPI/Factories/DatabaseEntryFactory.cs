using ParkingManagementAPI.Models.Database;

namespace ParkingManagementAPI.Factories
{
    public interface IDatabaseEntryFactory
    {
        public ParkingSpotAllocation GetParkingSpotAllocation(string vehicleRegistration, ParkingSpot parkingSpot, ChargeRate chargeRate);
    }

    public class DatabaseEntryFactory : IDatabaseEntryFactory
    {
        public ParkingSpotAllocation GetParkingSpotAllocation(string vehicleRegistration, ParkingSpot parkingSpot, ChargeRate chargeRate) =>
            new ParkingSpotAllocation { VehicleRegistration = vehicleRegistration, ParkingSpot = parkingSpot, ChargeRate = chargeRate };
    }
}
