namespace ParkingManagementAPI.Models.Database
{
    public class ParkingSpotAllocation
    {
        public Guid Id { get; set; }
        public DateTime StartDateTime { get; set; } = DateTime.Now;
        public DateTime? EndDateTime { get; set; }
        public required string VehicleRegistration {  get; set; }

        //Including this field, so the charge profits can be correctly analyzed over time. Otherwise if ChargeRate changed, all older allocations would be counted incorrectly.
        //Easier solution than making ChargeRate's have start-end dates as well.
        public double? FinalCharge { get; set; }

        public Guid ParkingSpotId { get; set; }
        public required ParkingSpot ParkingSpot { get; set; }

        //By this relation (instead of having double fields) I assume that if rate changes while vehicle is still parked -> it will have to pay by new rates when it exits
        public Guid ChargeRateId { get; set; }
        public required ChargeRate ChargeRate { get; set; }
    }
}
