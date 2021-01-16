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
            if (duration < 1)
                return Result<VehicleDuty>.Fail("Duration of workblock cant be less than 1");
            if (duration > 14400)
                return Result<VehicleDuty>.Fail("Duration of workblock cant be greater than 4 hours (14400 seconds)");
            if (numberOfWorkblocks < 1)
                return Result<VehicleDuty>.Fail("Number of workblocks cant be less than 1");

            var vehicleDutyDuration = GetVehicleDutyDuration();

            var remainingTime = GetRemainingTime(vehicleDutyDuration);
            if (remainingTime <= 0)
                return Result<VehicleDuty>.Fail("No room for more workblocks");

            var workblockList = CreateWorkblocks(remainingTime, duration, numberOfWorkblocks);

            this.Workblocks.AddRange(workblockList);

            return Result<VehicleDuty>.Ok(this);
        }

        private int GetVehicleDutyDuration()
        {
            this.Trips.Sort();
            this.Trips[0].PassingTimes.Sort();
            this.Trips[this.Trips.Count - 1].PassingTimes.Sort();

            var start = this.Trips[0].PassingTimes[0].TimeInstant;

            var lastTrip = this.Trips[this.Trips.Count - 1];
            var lastPassingTime = lastTrip.PassingTimes[lastTrip.PassingTimes.Count - 1];
            var end = lastPassingTime.TimeInstant;

            return end - start;
        }

        private int GetRemainingTime(int vehicleDutyDuration)
        {
            var remainingTime = vehicleDutyDuration;
            foreach (var wb in this.Workblocks)
            {
                var duration = wb.EndTime - wb.StartTime;
                remainingTime -= duration;
            }
            return remainingTime;
        }

        private List<Workblock> CreateWorkblocks(int remainingTime, int duration, int numberOfWorkblocks)
        {
            this.Workblocks.Sort();

            int startTime;
            if (this.Workblocks.Count == 0)
                startTime = 0;
            else
                startTime = this.Workblocks[this.Workblocks.Count - 1].EndTime;

            var workblockList = new List<Workblock>();

            while (numberOfWorkblocks > 0 && remainingTime > 0)
            {
                int endTime;
                if (duration > remainingTime)
                    endTime = startTime + remainingTime;
                else
                    endTime = startTime + duration;

                var triplist = FindWorkblockTrips(startTime, endTime);
                var result = Workblock.Create(startTime, endTime, triplist);
                workblockList.Add(result.Value);

                remainingTime -= endTime - startTime;
                numberOfWorkblocks--;
                startTime = endTime;
            }

            return workblockList;
        }

        private List<string> FindWorkblockTrips(int startTime, int endTime)
        {
            var list = new List<string>();

            foreach (var trip in this.Trips)
            {
                foreach (var passingtime in trip.PassingTimes)
                {
                    if (passingtime.TimeInstant >= startTime && passingtime.TimeInstant <= endTime)
                    {
                        list.Add(trip.Id.AsString());
                        break;
                    }
                }
            }

            return list;
        }
    }
}