using lapr5_masterdata_viagens.Shared;
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
        /// Creates the workblocks based on the given number of workblocks and duration of each workblock
        ///</summary>
        public Result<VehicleDuty> AddWorkBlocks(int duration, int numberOfWorkblocks)
        {
            return Result<VehicleDuty>.Ok(this);
        }
    }
}