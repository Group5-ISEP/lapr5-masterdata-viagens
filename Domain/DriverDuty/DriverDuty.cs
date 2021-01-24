using System;
using lapr5_masterdata_viagens.Shared;
using System.Globalization;
using System.Text.RegularExpressions;
using lapr5_masterdata_viagens.Domain.Shared;
using System.Collections.Generic;
using lapr5_masterdata_viagens.Domain.Trips;
using lapr5_masterdata_viagens.Domain.Workblocks;

namespace lapr5_masterdata_viagens.Domain.DriverDuties
{
    public class DriverDuty : Entity<DriverDutyId>, IAggregateRoot
    {
        // TODO: CREATION NEEDS REVIEW
        public string Name { get; private set; }
        public List<Workblock> Workblocks { get; private set; }


        private DriverDuty()
        {
            //FOR ORM
        }
        private DriverDuty(string id, string name, List<Workblock> workblocks)
        {
            this.Id = new DriverDutyId(id);
            this.Name = name;
            this.Workblocks = workblocks;
        }

        public static Result<DriverDuty> Create(string name, List<Workblock> workblocks, string id = null)
        {
            DriverDuty DriverDuty = new DriverDuty(id, name, workblocks);
            return Result<DriverDuty>.Ok(DriverDuty);
        }
    }
}