using System.ComponentModel.DataAnnotations;

namespace ParkingManagementAPI.Models.Database
{
    public class ParkingSpot
    {
        [Key]
        public Guid Id { get; set; }
        public required int ParkingSpotNumber { get; set; }
        public List<ParkingSpotAllocation>? ParkingSpotAllocations { get; set; }
    }
}
