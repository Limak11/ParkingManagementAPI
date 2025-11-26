using System.Text.Json.Serialization;

namespace ParkingManagementAPI.Models.Http.Requests
{
    public class VehicleExitRequest
    {
        [JsonPropertyName("VehicleReg")]
        public string? VehicleRegistration { get; set; }
    }
}
