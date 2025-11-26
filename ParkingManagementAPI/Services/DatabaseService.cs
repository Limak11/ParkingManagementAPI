using Microsoft.EntityFrameworkCore;
using ParkingManagementAPI.Context;
using ParkingManagementAPI.Exceptions;
using ParkingManagementAPI.Models.Database;

namespace ParkingManagementAPI.Services
{
    public class DatabaseService
    {
        private readonly ParkingContext _context;
        public DatabaseService(ParkingContext context)
        {
            _context = context;
        }

        public async Task<List<ParkingSpot>> GetAvailableSpotsAsync()
        {
            var allSpots = await GetAllSpotsAsync();
            var occupiedSpots = await GetOccupiedSpotsAsync();

            var availableSpots = allSpots.Except(occupiedSpots);

            return  availableSpots.ToList();
        }

        public async Task<List<ParkingSpot>> GetOccupiedSpotsAsync()
        {
            List<ParkingSpot> occupiedSpots = new();

            foreach (var allocation in await GetCurrentAllocationsAsync())
            {
                occupiedSpots.Add(allocation.ParkingSpot);
            }

            return occupiedSpots;
        }

        public async Task<ChargeRate?> GetChargeRateByVehicleTypeAsync(int vehicleType) => await _context.ChargeRates.Where(r => r.VehicleType == vehicleType).FirstOrDefaultAsync();

        public async Task<ParkingSpotAllocation> SaveAllocationAsync(ParkingSpotAllocation allocation)
        {
            var parkingSpotAvailable = await CheckIfParkingSpotIsFreeAsync(allocation.ParkingSpotId);
            if (parkingSpotAvailable == false) throw new IncorrectParkingException($"Can't allocate vehicle {allocation.VehicleRegistration}. Parking spot {allocation.ParkingSpot.ParkingSpotNumber} is already taken");

            await _context.ParkingSpotAllocations.AddAsync(allocation);
            await _context.SaveChangesAsync();

            return allocation;
        }

        public async Task<ParkingSpotAllocation> CloseAllocationAsync(ParkingSpotAllocation allocation)
        {
            _context.Update(allocation);
            await _context.SaveChangesAsync();

            return allocation;
        }

        public async Task<bool> AddParkingSpotAsync(int parkingSpotNumber)
        {
            var checkIfParkingSpotExists = await _context.ParkingSpots.Where(s => s.ParkingSpotNumber == parkingSpotNumber).FirstOrDefaultAsync();
            if (checkIfParkingSpotExists is not null) return false;

            await _context.ParkingSpots.AddAsync(new ParkingSpot { ParkingSpotNumber = parkingSpotNumber });

            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> AddChargeRateAsync(int vehicleType, double perMinuteCharge, double additionalCharge)
        {
            var checkIfVehicleTypeExists = await _context.ChargeRates.Where(r => r.VehicleType == vehicleType).FirstOrDefaultAsync();
            if(checkIfVehicleTypeExists is not null) return false;

            await _context.ChargeRates.AddAsync(new ChargeRate { VehicleType = vehicleType, ChargePerMinute = perMinuteCharge, AdditionalCharge = additionalCharge });

            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<ParkingSpotAllocation?> GetCurrentAllocationByVehicleRegistrationAsync(string vehicleRegistration) => 
            await _context.ParkingSpotAllocations.Where(a => a.VehicleRegistration == vehicleRegistration && a.EndDateTime == null).Include(a => a.ChargeRate).FirstOrDefaultAsync();

        private async Task<List<ParkingSpotAllocation>> GetCurrentAllocationsAsync() => await _context.ParkingSpotAllocations.Where(a => a.EndDateTime == null).ToListAsync();

        private async Task<List<ParkingSpot>> GetAllSpotsAsync() => await _context.ParkingSpots.ToListAsync();

        private async Task<bool> CheckIfParkingSpotIsFreeAsync(Guid id) => await _context.ParkingSpotAllocations.Where(a => a.ParkingSpotId == id && a.EndDateTime == null).FirstOrDefaultAsync() is null;
    }
}
