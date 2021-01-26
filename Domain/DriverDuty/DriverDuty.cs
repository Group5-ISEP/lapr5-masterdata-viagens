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
        public string Name { get; private set; }
        public List<Workblock> Workblocks { get; private set; }


        private DriverDuty()
        {
            //FOR ORM
        }
        private DriverDuty(string id, string name)
        {
            this.Id = new DriverDutyId(id);
            this.Name = name;
            this.Workblocks = new List<Workblock>();
        }

        public static Result<DriverDuty> Create(string name, string id = null)
        {
            if (name == null || name.Length == 0)
                return Result<DriverDuty>.Fail("Name cant be empty");
            DriverDuty DriverDuty = new DriverDuty(id, name);
            return Result<DriverDuty>.Ok(DriverDuty);
        }
    }
}