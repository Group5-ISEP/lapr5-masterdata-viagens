using System.Collections.Generic;

namespace lapr5_masterdata_viagens.Domain.VehicleDuties
{
    public class VehicleDutyDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Trips { get; set; }
        public List<string> Workblocks { get; set; }

    }
}