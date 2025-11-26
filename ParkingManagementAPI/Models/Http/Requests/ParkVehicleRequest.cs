using System.Text.Json.Serialization;

namespace ParkingManagementAPI.Models.Http.Requests
{
    public class ParkVehicleRequest
    {
        [JsonPropertyName("VehicleReg")]
        public string? VehicleRegistration {  get; set; }

        [JsonPropertyName("VehicleType")]
        public int? VehicleType { get; set; }
    }
}
