using Microsoft.EntityFrameworkCore;
using ParkingManagementAPI.Models.Database;

namespace ParkingManagementAPI.Context
{
    public class ParkingContext : DbContext
    {
        public ParkingContext() : base()
        {
        }

        public ParkingContext(DbContextOptions<ParkingContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ParkingSpot>()
                .HasMany(s => s.ParkingSpotAllocations)
                .WithOne(a => a.ParkingSpot);

            modelBuilder.Entity<ChargeRate>()
                .HasMany(r => r.ParkingSpotAllocations)
                .WithOne(a => a.ChargeRate);

            modelBuilder.Entity<ChargeRate>().HasData(
                new ChargeRate { Id = new Guid("268d279a-e47e-4c93-9eb3-0a13142cad03"), VehicleTypeDisplayName = "Small", VehicleType = 1, ChargePerMinute = 0.1, AdditionalCharge = 1 },
                new ChargeRate { Id = new Guid("b698623a-39ac-43a9-9b50-031f1a8605cb"), VehicleTypeDisplayName = "Medium", VehicleType = 2, ChargePerMinute = 0.2, AdditionalCharge = 1 },
                new ChargeRate { Id = new Guid("c2ca03a5-e2f8-4c78-bde5-4eaa218f06da"), VehicleTypeDisplayName = "Large", VehicleType = 3, ChargePerMinute = 0.4, AdditionalCharge = 1 }
                );

            // No requirement for many spots to be seeded, so sticking to the most simple solution with HasData. Not really sustainable for more spots to hardcode this many objects
            modelBuilder.Entity<ParkingSpot>().HasData(
                new ParkingSpot { Id = new Guid("658f6909-fda6-4132-aefc-c66cf9d00ebe"), ParkingSpotNumber = 1},
                new ParkingSpot { Id = new Guid("8830eaf5-60bd-4715-9f3c-920379ba1d7c"), ParkingSpotNumber = 2},
                new ParkingSpot { Id = new Guid("495b53ed-2944-4e49-a2f2-087bfc8cb3c8"), ParkingSpotNumber = 3},
                new ParkingSpot { Id = new Guid("513c30f8-d92d-466b-ab88-1c1d5416f223"), ParkingSpotNumber = 4},
                new ParkingSpot { Id = new Guid("bfcde73a-c71f-46f5-a2ab-9c89b50c0136"), ParkingSpotNumber = 5}
                );
        }

        public DbSet<ParkingSpot> ParkingSpots { get; set; }
        public DbSet<ChargeRate> ChargeRates { get; set; }
        public DbSet<ParkingSpotAllocation> ParkingSpotAllocations { get; set; }
    }
}
