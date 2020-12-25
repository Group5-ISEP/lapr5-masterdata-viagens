using Microsoft.EntityFrameworkCore;
using lapr5_masterdata_viagens.Domain.Vehicles;
using lapr5_masterdata_viagens.Domain.Drivers;
using System;
using System.Collections.Generic;

namespace lapr5_masterdata_viagens.Infrastructure
{
    public class ViagensDbContext : DbContext
    {
        public ViagensDbContext(DbContextOptions<ViagensDbContext> options)
            : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<Driver> Drivers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Write Fluent API configurations here

            //Property Configurations

            ConfigureVehicles(modelBuilder);
            ConfigureDrivers(modelBuilder);
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

        private void ConfigureDrivers(ModelBuilder modelBuilder)
        {
            //ID
            modelBuilder.Entity<Driver>()
                .HasKey(d => d.Id);

            //Name
            modelBuilder.Entity<Driver>()
                .Property(d => d.Name)
                .IsRequired();

            //Birth date
            modelBuilder.Entity<Driver>()
                .Property(d => d.BirthDate)
                .IsRequired();

            //License number
            modelBuilder.Entity<Driver>()
                .Property(d => d.DriverLicenseNumber)
                .IsRequired();
            modelBuilder.Entity<Driver>()
                .HasIndex(d => d.DriverLicenseNumber)
                .IsUnique();

            //License expiration date
            modelBuilder.Entity<Driver>()
                .Property(d => d.DriverLicenseExpirationDate)
                .IsRequired();

            //Type IDs list
            modelBuilder.Entity<Driver>()
                .Property(d => d.TypesIDs)
                .IsRequired();
            modelBuilder.Entity<Driver>()
                .Property(d => d.TypesIDs)
                .HasConversion(
                    v => string.Join(',', v),
                    v => new List<string>(v.Split(',', StringSplitOptions.RemoveEmptyEntries))
                );

        }
    }


}