﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using lapr5_masterdata_viagens.Infrastructure;

namespace lapr5_masterdata_viagens.Migrations
{
    [DbContext(typeof(ViagensDbContext))]
    partial class ViagensDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("lapr5_masterdata_viagens.Domain.Drivers.Driver", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DriverLicenseExpirationDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("DriverLicenseNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TypesIDs")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DriverLicenseNumber")
                        .IsUnique();

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("lapr5_masterdata_viagens.Domain.Trips.PassingTime", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("NodeID")
                        .HasColumnType("TEXT");

                    b.Property<int>("TimeInstant")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TripId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("TripId");

                    b.ToTable("PassingTimes");
                });

            modelBuilder.Entity("lapr5_masterdata_viagens.Domain.Trips.Trip", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("LineID")
                        .HasColumnType("TEXT");

                    b.Property<string>("Orientation")
                        .HasColumnType("TEXT");

                    b.Property<string>("PathID")
                        .HasColumnType("TEXT");

                    b.Property<string>("VehicleDutyId")
                        .HasColumnType("TEXT");

                    b.Property<string>("WorkblockId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("VehicleDutyId");

                    b.HasIndex("WorkblockId");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("lapr5_masterdata_viagens.Domain.VehicleDuties.VehicleDuty", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("VehicleDuties");
                });

            modelBuilder.Entity("lapr5_masterdata_viagens.Domain.Vehicles.Vehicle", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("CarPlateCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ServiceStartDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("VIN")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("VehicleTypeID")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CarPlateCode")
                        .IsUnique();

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("lapr5_masterdata_viagens.Domain.Workblocks.Workblock", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("EndTime")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StartTime")
                        .HasColumnType("INTEGER");

                    b.Property<string>("VehicleDutyId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("VehicleDutyId");

                    b.ToTable("Workblocks");
                });

            modelBuilder.Entity("lapr5_masterdata_viagens.Domain.Trips.PassingTime", b =>
                {
                    b.HasOne("lapr5_masterdata_viagens.Domain.Trips.Trip", null)
                        .WithMany("PassingTimes")
                        .HasForeignKey("TripId");
                });

            modelBuilder.Entity("lapr5_masterdata_viagens.Domain.Trips.Trip", b =>
                {
                    b.HasOne("lapr5_masterdata_viagens.Domain.VehicleDuties.VehicleDuty", null)
                        .WithMany("Trips")
                        .HasForeignKey("VehicleDutyId");

                    b.HasOne("lapr5_masterdata_viagens.Domain.Workblocks.Workblock", null)
                        .WithMany("Trips")
                        .HasForeignKey("WorkblockId");
                });

            modelBuilder.Entity("lapr5_masterdata_viagens.Domain.Workblocks.Workblock", b =>
                {
                    b.HasOne("lapr5_masterdata_viagens.Domain.VehicleDuties.VehicleDuty", null)
                        .WithMany("Workblocks")
                        .HasForeignKey("VehicleDutyId");
                });

            modelBuilder.Entity("lapr5_masterdata_viagens.Domain.Trips.Trip", b =>
                {
                    b.Navigation("PassingTimes");
                });

            modelBuilder.Entity("lapr5_masterdata_viagens.Domain.VehicleDuties.VehicleDuty", b =>
                {
                    b.Navigation("Trips");

                    b.Navigation("Workblocks");
                });

            modelBuilder.Entity("lapr5_masterdata_viagens.Domain.Workblocks.Workblock", b =>
                {
                    b.Navigation("Trips");
                });
#pragma warning restore 612, 618
        }
    }
}
