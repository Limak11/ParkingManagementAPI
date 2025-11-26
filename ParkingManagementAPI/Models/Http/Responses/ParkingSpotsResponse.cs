using System.Text.Json.Serialization;

namespace ParkingManagementAPI.Models.Http.Responses
{
    public class ParkingSpotsResponse
    {
        [JsonPropertyName("AvailableSpaces")]
        public int AvailableSpots { get; set; }

        [JsonPropertyName("OccupiedSpaces")]
        public int OccupiedSpots { get; set; }
    }
}
