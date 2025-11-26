using System.Text.Json.Serialization;

namespace ParkingManagementAPI.Models.Http.Responses
{
    public class ParkVehicleResponse
    {
        [JsonPropertyName("VehicleReg")]
        public required string VehicleRegistration { get; set; }

        [JsonPropertyName("SpaceNumber")]
        public int ParkingSpotNumber { get; set; }

        [JsonPropertyName("TimeIn")]
        public DateTime StartDateTime { get; set; }
    }
}
