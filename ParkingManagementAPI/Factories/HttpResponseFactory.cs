using ParkingManagementAPI.Models.Database;
using ParkingManagementAPI.Models.Http.Responses;

namespace ParkingManagementAPI.Factories
{
    public interface IHttpResponseFactory
    {
        public ParkingSpotsResponse GetParkingSpotsResponse(int availableSpots, int occupiedSpots);
        public ParkVehicleResponse GetParkVehicleResponse(ParkingSpotAllocation allocation);
        public VehicleExitResponse GetExitVehicleResponse(ParkingSpotAllocation allocation, double finalCharge, DateTime endDateTime);
    }

    public class HttpResponseFactory : IHttpResponseFactory
    {
        public ParkingSpotsResponse GetParkingSpotsResponse(int availableSpots, int occupiedSpots) =>
            new ParkingSpotsResponse { AvailableSpots = availableSpots, OccupiedSpots = occupiedSpots };

        public ParkVehicleResponse GetParkVehicleResponse(ParkingSpotAllocation allocation) =>
            new ParkVehicleResponse { ParkingSpotNumber = allocation.ParkingSpot.ParkingSpotNumber, StartDateTime = allocation.StartDateTime, VehicleRegistration = allocation.VehicleRegistration };

        public VehicleExitResponse GetExitVehicleResponse(ParkingSpotAllocation allocation, double finalCharge, DateTime endDateTime) =>
            new VehicleExitResponse { VehicleRegistration = allocation.VehicleRegistration, VehicleCharge = finalCharge, StartDateTime = allocation.StartDateTime, EndDateTime = endDateTime };

    }
}
