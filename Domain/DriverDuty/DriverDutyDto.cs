using System.Collections.Generic;
using lapr5_masterdata_viagens.Domain.Workblocks;

namespace lapr5_masterdata_viagens.Domain.DriverDuties
{
    public class DriverDutyDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<WorkblockDto> Workblocks { get; set; }

    }
}