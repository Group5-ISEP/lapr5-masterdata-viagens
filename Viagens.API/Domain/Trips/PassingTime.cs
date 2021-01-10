using System;
using lapr5_masterdata_viagens.Domain.Shared;
using lapr5_masterdata_viagens.Shared;

namespace lapr5_masterdata_viagens.Domain.Trips
{
    public class PassingTime : Entity<PassingTimeId>, IAggregateRoot
    {
        public int TimeInstant { get; private set; }

        public string NodeID { get; private set; }


        private PassingTime()
        {
            //FOR ORM
        }
        private PassingTime(int timeInstant, string nodeId, string id = null)
        {
            this.Id = new PassingTimeId(id);
            this.TimeInstant = timeInstant;
            this.NodeID = nodeId;
        }

        public static Result<PassingTime> Create(int timeInstant, string nodeId, string id = null)
        {
            if (timeInstant < 0)
                return Result<PassingTime>.Fail("Time instant cant be less than zero");
            if (timeInstant > 86400)
                return Result<PassingTime>.Fail("Time instant cant be more than 86400 (24 hours)");
            if (nodeId == null)
                return Result<PassingTime>.Fail("Passing time node id cant be null");

            return Result<PassingTime>.Ok(new PassingTime(timeInstant, nodeId, id));
        }
    }
}