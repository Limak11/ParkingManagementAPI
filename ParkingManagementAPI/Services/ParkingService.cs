using ParkingManagementAPI.Exceptions;
using ParkingManagementAPI.Factories;
using ParkingManagementAPI.Models.Http.Requests;
using ParkingManagementAPI.Models.Http.Responses;
using ParkingManagementAPI.Services.Helpers;

namespace ParkingManagementAPI.Services
{
    public class ParkingService
    {
        private readonly DatabaseService _databaseService;
        private readonly ChargeCalculationHelper _chargeHelper;
        private readonly IHttpResponseFactory _httpResponseFactory;
        private readonly IDatabaseEntryFactory _dbEntryFactory;

        public ParkingService(DatabaseService databaseService, ChargeCalculationHelper chargeHelper, IHttpResponseFactory httpResponseFactory, IDatabaseEntryFactory dbEntryFactory)
        {
            _databaseService = databaseService;
            _chargeHelper = chargeHelper;
            _httpResponseFactory = httpResponseFactory;
            _dbEntryFactory = dbEntryFactory;
        }

        public async Task<ParkingSpotsResponse> GetParkingSpotCountsAsync()
        {
            var availableSpots = await _databaseService.GetAvailableSpotsAsync();
            var occupiedSpots = await _databaseService.GetOccupiedSpotsAsync();

            return _httpResponseFactory.GetParkingSpotsResponse(availableSpots.Count, occupiedSpots.Count);
        }

        public async Task<ParkVehicleResponse> ProcessVehicleParkingAsync(ParkVehicleRequest request)
        {
            //Check if that registration already has a spot taken?
            var currentAllocation = await _databaseService.GetCurrentAllocationByVehicleRegistrationAsync(request.VehicleRegistration!);

            //vehicle with given registration is parked
            if (currentAllocation != null) throw new IncorrectParkingException("Vehicle is already parked");

            var availableSpots = await _databaseService.GetAvailableSpotsAsync();
            if (availableSpots == null || availableSpots.Count == 0) throw new IncorrectParkingException("Not spots available");

            var selectedSpot = availableSpots.OrderBy(s => s.ParkingSpotNumber).First();

            var chargeRate = await _databaseService.GetChargeRateByVehicleTypeAsync((int)request.VehicleType!);
            if (chargeRate == null) throw new IncorrectParkingException("No charge rate exists for vehicle of that type");

            var allocationToCreate = _dbEntryFactory.GetParkingSpotAllocation(request.VehicleRegistration!, selectedSpot, chargeRate);
            var createdAllocation = await _databaseService.SaveAllocationAsync(allocationToCreate);

            return _httpResponseFactory.GetParkVehicleResponse(createdAllocation);
        }

        public async Task<VehicleExitResponse> ProcessVehicleExitAsync(VehicleExitRequest request)
        {
            //Check if that registration already has a spot taken?
            var currentAllocation = await _databaseService.GetCurrentAllocationByVehicleRegistrationAsync(request.VehicleRegistration!);

            if (currentAllocation == null) throw new IncorrectParkingException("No allocation found for vehicle with such registration");

            var parkingEndDateTime = DateTime.Now;
            currentAllocation.EndDateTime = parkingEndDateTime;

            //calculate charge
            var finalCharge = _chargeHelper.CalculateFinalCharge(currentAllocation);
            currentAllocation.FinalCharge = finalCharge;

            //free parking spot
            currentAllocation = await _databaseService.CloseAllocationAsync(currentAllocation);

            return _httpResponseFactory.GetExitVehicleResponse(currentAllocation, finalCharge, parkingEndDateTime);
        }
    }
}
