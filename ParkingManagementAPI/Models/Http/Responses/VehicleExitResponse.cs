using System.Text.Json.Serialization;

namespace ParkingManagementAPI.Models.Http.Responses
{
    public class VehicleExitResponse
    {
        [JsonPropertyName("VehicleReg")]
        public required string VehicleRegistration { get; set; }

        [JsonPropertyName("VehicleCharge")]
        public double VehicleCharge { get; set; }

        [JsonPropertyName("TimeIn")]
        public DateTime StartDateTime { get; set; }

        [JsonPropertyName("TimeOut")]
        public DateTime EndDateTime { get; set; }
    }
}
