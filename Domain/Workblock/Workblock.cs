using lapr5_masterdata_viagens.Shared;
using lapr5_masterdata_viagens.Domain.Shared;
using System.Collections.Generic;
using System;

namespace lapr5_masterdata_viagens.Domain.Workblocks
{
    public class Workblock : Entity<WorkblockId>, IAggregateRoot, IComparable<Workblock>
    {
        public int StartTime { get; private set; }
        public int EndTime { get; private set; }
        public List<string> TripsIDs { get; private set; }


        private Workblock()
        {
            //FOR ORM
        }
        private Workblock(int starttime, int endtime, List<string> trips, string id = null)
        {
            this.Id = new WorkblockId(id);
            this.StartTime = starttime;
            this.EndTime = endtime;
            this.TripsIDs = trips;
        }
        public static Result<Workblock> Create(int starttime, int endtime, List<string> trips, string id = null)
        {
            if (starttime < 0)
                return Result<Workblock>.Fail("Workblock start time cant be less than zero");
            if (endtime <= starttime)
                return Result<Workblock>.Fail("Workblock end time cant be less than start time");
            if (endtime > 86400)
                return Result<Workblock>.Fail("Workblock end time cant be more than 86400 (24 hours)");
            if (trips == null)
                return Result<Workblock>.Fail("Trips list cant be null");

            Workblock Workblock = new Workblock(starttime, endtime, trips, id);
            return Result<Workblock>.Ok(Workblock);
        }

        public int CompareTo(Workblock other)
        {
            if (this.StartTime < other.StartTime)
                return -1;

            return 1;
        }
    }
}