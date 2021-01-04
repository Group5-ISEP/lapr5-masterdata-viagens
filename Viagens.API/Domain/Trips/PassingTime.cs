using System;
using lapr5_masterdata_viagens.Domain.Shared;

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
        public PassingTime(int timeInstant, string nodeId)
        {
            this.Id = new PassingTimeId(Guid.NewGuid().ToString());
            this.TimeInstant = timeInstant;
            this.NodeID = nodeId;
        }

        public PassingTime(string id, int timeInstant, string nodeId)
        {
            this.Id = new PassingTimeId(id);
            this.TimeInstant = timeInstant;
            this.NodeID = nodeId;
        }
    }
}