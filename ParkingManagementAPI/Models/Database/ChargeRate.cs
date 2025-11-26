using System.ComponentModel.DataAnnotations;

namespace ParkingManagementAPI.Models.Database
{
    public class ChargeRate
    {
        [Key]
        public Guid Id { get; set; }
        public string? VehicleTypeDisplayName { get; set; }
        public required int VehicleType { get; set; }
        public required double ChargePerMinute { get; set; }
        public required double AdditionalCharge {  get; set; }

        public List<ParkingSpotAllocation>? ParkingSpotAllocations { get; set; }
    }
}
