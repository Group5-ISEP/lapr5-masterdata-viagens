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
        private VehicleDuty(string name, List<Trip> trips, string id = null)
        {
            this.Id = new VehicleDutyId(id);
            this.Name = name;
            this.Trips = trips;
            this.Workblocks = new List<Workblock>();
        }

        public static Result<VehicleDuty> Create(string name, List<Trip> trips, string id = null)
        {
            if (name == null)
                return Result<VehicleDuty>.Fail("Vehicle duty name cant be null");
            if (name.Length == 0)
                return Result<VehicleDuty>.Fail("Vehicle duty name cant be empty");
            if (trips == null)
                return Result<VehicleDuty>.Fail("Trips list cant be null");
            if (trips.Count == 0)
                return Result<VehicleDuty>.Fail("Trips list cant be empty");
            if (id != null && id.Length == 0)
                return Result<VehicleDuty>.Fail("Vehicle duty id cant be empty");

            VehicleDuty VehicleDuty = new VehicleDuty(name, trips, id);
            return Result<VehicleDuty>.Ok(VehicleDuty);
        }

        ///<summary>
        /// Adds the workblocks in workblocks parameter list that the attribute list doesnt have
        ///</summary>
        public void AddWorkBlocks(List<Workblock> workblocks)
        {
            foreach (var wb in workblocks)
            {
                if (this.Workblocks.Contains(wb) == false)
                    this.Workblocks.Add(wb);
            }
        }
    }
}