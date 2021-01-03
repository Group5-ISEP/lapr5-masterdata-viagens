using System;
using lapr5_masterdata_viagens.Shared;
using System.Globalization;
using System.Text.RegularExpressions;
using lapr5_masterdata_viagens.Domain.Shared;
using System.Collections.Generic;
using lapr5_masterdata_viagens.Domain.Trips;
using lapr5_masterdata_viagens.Domain.Workblocks;

namespace lapr5_masterdata_viagens.Domain.VehicleDuties
{
    public class VehicleDuty : Entity<VehicleDutyId>, IAggregateRoot
    {
        public string Name { get; private set; }
        public List<Trip> Trips { get; private set; }
        public List<Workblock> Workblocks { get; private set; }


        private VehicleDuty()
        {
            //FOR ORM
        }
        private VehicleDuty(string name, List<Trip> trips)
        {
            this.Id = new VehicleDutyId(Guid.NewGuid().ToString());
            this.Name = name;
            this.Trips = trips;
            this.Workblocks = new List<Workblock>();
        }

        public static Result<VehicleDuty> Create(string name, List<Trip> trips)
        {
            VehicleDuty VehicleDuty = new VehicleDuty(name, trips);
            return Result<VehicleDuty>.Ok(VehicleDuty);
        }
    }
}