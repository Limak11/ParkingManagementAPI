using ParkingManagementAPI.Models.Http.Requests;
using ParkingManagementAPI.Services.Helpers;


namespace ParkingManagementAPI.Tests.Helpers
{
    public class RequestVerificationHelpersTest
    {
        private readonly RequestVerificationHelper _helper;

        public RequestVerificationHelpersTest()
        {
            _helper = new();
        }

        [Theory]
        [InlineData("DW12345", 1, true)]
        [InlineData("DW1234567", 2, true)]
        [InlineData("D", 3, true)]
        [InlineData("", 2, false)]
        [InlineData(null, 1, false)]
        [InlineData(null, null, false)]
        [InlineData(null, -1, false)]
        [InlineData("DW12345", 0, false)]
        [InlineData("DW12345", null, false)]
        [InlineData("", null, false)]
        [InlineData("", -2, false)]
        public void If_VerifyParkVehicleRequest_Matches(string? vehicleRegistration, int? vehicleType, bool desiredResult)
        {
            var request = new ParkVehicleRequest { VehicleRegistration = vehicleRegistration, VehicleType = vehicleType };
            var result = _helper.VerifyParkVehicleRequest(request);

            Assert.Equal(desiredResult, result);
        }

        [Theory]
        [InlineData("DW12345", true)]
        [InlineData("D", true)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public void If_VerifyVehicleExitRequest_Matches(string? vehicleRegistration, bool desiredResult)
        {
            var request = new VehicleExitRequest { VehicleRegistration = vehicleRegistration };
            var result = _helper.VerifyVehicleExitRequest(request);

            Assert.Equal(desiredResult, result);
        }

        [Fact]
        public void VerifyParkVehicleRequest_RequestIsNull_False()
        {
            var result = _helper.VerifyParkVehicleRequest(null);

            Assert.False(result);
        }


        [Fact]
        public void VerifyVehicleExitRequest_RequestIsNull_False()
        {
            var result = _helper.VerifyVehicleExitRequest(null);

            Assert.False(result);
        }
    }
}