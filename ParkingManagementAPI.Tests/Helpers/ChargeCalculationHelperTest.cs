using ParkingManagementAPI.Exceptions;
using ParkingManagementAPI.Models.Database;
using ParkingManagementAPI.Services.Helpers;

namespace ParkingManagementAPI.Tests.Helpers
{
    public class ChargeCalculationHelperTest
    {

        private readonly ChargeCalculationHelper _helper;

        public ChargeCalculationHelperTest()
        {
            _helper = new();
        }

        [Fact]
        public void CalculateFinalCharge_NoEndDate_Throws_IncorrectParkingException()
        {
            var allocation = new ParkingSpotAllocation
            {
                VehicleRegistration = "DW12345",
                ChargeRate = new ChargeRate { AdditionalCharge = 1, ChargePerMinute = 0.1, VehicleType = 1},
                ParkingSpot = new ParkingSpot { ParkingSpotNumber = 1}
            };

            Assert.Throws<IncorrectParkingException>(() =>  _helper.CalculateFinalCharge(allocation));
        }

        [Fact]
        public void CalculateFinalCharge_FinalCharge_Matches()
        {
            var desiredFinalCharge = 3;

            var allocation = new ParkingSpotAllocation
            {
                VehicleRegistration = "DW12345",
                ChargeRate = new ChargeRate { AdditionalCharge = 1, ChargePerMinute = 0.1, VehicleType = 1 },
                ParkingSpot = new ParkingSpot { ParkingSpotNumber = 1 },
                StartDateTime = new DateTime(2025, 12, 01, 12, 0, 37),
                EndDateTime = new DateTime(2025, 12, 01, 12, 10, 37)
            };

            var resultCharge = _helper.CalculateFinalCharge(allocation);

            Assert.Equal(desiredFinalCharge, resultCharge);
        }
    }
}
