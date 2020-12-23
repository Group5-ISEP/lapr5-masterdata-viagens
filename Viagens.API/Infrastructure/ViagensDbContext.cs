using Microsoft.EntityFrameworkCore;
using lapr5_masterdata_viagens.Domain.Vehicles;

namespace lapr5_masterdata_viagens.Infrastructure
{
    public class ViagensDbContext : DbContext
    {
        public ViagensDbContext(DbContextOptions<ViagensDbContext> options)
            : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Write Fluent API configurations here

            //Property Configurations

            ConfigureVehicles(modelBuilder);
        }

        private void ConfigureVehicles(ModelBuilder modelBuilder)
        {
            //ID
            modelBuilder.Entity<Vehicle>()
                .HasKey(v => v.Id);

            //Plate code
            modelBuilder.Entity<Vehicle>()
                .Property(v => v.CarPlateCode)
                .IsRequired();
            modelBuilder.Entity<Vehicle>()
                .HasIndex(v => v.CarPlateCode)
                .IsUnique();

            //VIN
            modelBuilder.Entity<Vehicle>()
                .Property(v => v.VIN)
                .IsRequired();

            //Service Start Date
            modelBuilder.Entity<Vehicle>()
                .Property(v => v.ServiceStartDate)
                .IsRequired();

            //Type id
            modelBuilder.Entity<Vehicle>()
                .Property(v => v.VehicleTypeID)
                .IsRequired();
        }
    }
}