using System;
using lapr5_masterdata_viagens.Shared;
using System.Globalization;
using System.Text.RegularExpressions;
using lapr5_masterdata_viagens.Domain.Shared;
using System.Collections.Generic;
using lapr5_masterdata_viagens.Domain.Trips;

namespace lapr5_masterdata_viagens.Domain.Workblocks
{
    public class Workblock : Entity<WorkblockId>, IAggregateRoot
    {
        public int StartTime { get; private set; }
        public int EndTime { get; private set; }
        public List<Trip> Trips { get; private set; }


        private Workblock()
        {
            //FOR ORM
        }
        private Workblock(string id, int starttime, int endtime, List<Trip> trips)
        {
            this.Id = new WorkblockId(id);
            this.StartTime = starttime;
            this.EndTime = endtime;
            this.Trips = trips;
        }

        public static Result<Workblock> Create(int starttime, int endtime, List<Trip> trips, string id = null)
        {
            Workblock Workblock = new Workblock(id, starttime, endtime, trips);
            return Result<Workblock>.Ok(Workblock);
        }
    }
}