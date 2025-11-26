using Microsoft.AspNetCore.Mvc;
using ParkingManagementAPI.Exceptions;
using ParkingManagementAPI.Models.Http.Requests;
using ParkingManagementAPI.Services;
using ParkingManagementAPI.Services.Helpers;
using System.Text.Json;

namespace ParkingManagementAPI.Controllers
{
    //Assuming no auth
    [ApiController]
    [Route("[controller]")]
    public class ParkingController : ControllerBase
    {
        private readonly ILogger<ParkingController> _logger;
        private readonly ParkingService _service;
        private readonly RequestVerificationHelper _verificationHelper;

        public ParkingController(ILogger<ParkingController> logger, ParkingService service, RequestVerificationHelper verificationHelper)
        {
            _logger = logger;
            _service = service;
            _verificationHelper = verificationHelper;
        }

        [HttpGet]
        [Route("ping")]
        public IActionResult Ping()
        {
            _logger.LogInformation($"Ping endpoint executed at {DateTime.Now}");
            return Ok("Hello from the other side");
        }

        [HttpGet]
        public async Task<IActionResult> GetParkingSpotsAsync()
        {
            _logger.LogInformation($"GetParkingSpotsAsync endpoint triggered at {DateTime.Now}");
            var response = await _service.GetParkingSpotCountsAsync();
            var jsonResponse = JsonSerializer.Serialize(response);

            return Ok(jsonResponse);
        }

        [HttpPost]
        public async Task<IActionResult> ParkVehicleAsync([FromBody]ParkVehicleRequest request)
        {
            _logger.LogInformation($"ParkVehicleAsync endpoint triggered at {DateTime.Now}");

            if (!_verificationHelper.VerifyParkVehicleRequest(request))
            {
                _logger.LogError("ParkVehicleAsync request failed verification");
                return BadRequest("Request body is incorrect.");
            }

            try
            {
                var response = await _service.ProcessVehicleParkingAsync(request);
                var jsonResponse = JsonSerializer.Serialize(response);

                return Ok(jsonResponse);

            }
            catch(IncorrectParkingException ex)
            {
                _logger.LogError($"ParkVehicleAsync failed due to exception: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("exit")]
        public async Task<IActionResult> VehicleExitAsync([FromBody]VehicleExitRequest request)
        {
            _logger.LogInformation($"VehicleExitAsync endpoint triggered at {DateTime.Now}");

            if (!_verificationHelper.VerifyVehicleExitRequest(request))
            {
                _logger.LogError("VehicleExitAsync request failed verification");
                return BadRequest("Request body is incorrect.");
            }

            try
            {
                var response = await _service.ProcessVehicleExitAsync(request);
                var jsonResponse = JsonSerializer.Serialize(response);

                return Ok(jsonResponse);
            }
            catch (IncorrectParkingException ex)
            {
                _logger.LogError($"VehicleExitAsync failed due to exception: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
