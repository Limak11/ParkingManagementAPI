using ParkingManagementAPI.Models.Http.Requests;

namespace ParkingManagementAPI.Services.Helpers
{
    public class RequestVerificationHelper
    {
        public RequestVerificationHelper() { }

        public bool VerifyParkVehicleRequest(ParkVehicleRequest? request) => (request is not null) && VehicleRegistrationVerification(request.VehicleRegistration) && VehicleTypeVerification(request.VehicleType);

        public bool VerifyVehicleExitRequest(VehicleExitRequest? request) => (request is not null) && VehicleRegistrationVerification(request.VehicleRegistration);

        //Assuming vehicle registration format is verifed in frontend
        private bool VehicleRegistrationVerification(string? vehicleRegistration) => (vehicleRegistration is not null) && (vehicleRegistration.Length > 0);

        private bool VehicleTypeVerification(int? vehicleType) => (vehicleType is not null) && (vehicleType > 0);
    }
}
