using System.Collections.Generic;
using System.Linq;
namespace lapr5_masterdata_viagens.Domain.Trips
{
    public class TripMapper
    {
        public static TripDTO ToDto(Trip trip)
        {
            TripDTO dto = new TripDTO();
            dto.Id = trip.Id.AsString();
            dto.LineID = trip.LineID;
            dto.Orientation = trip.Orientation;
            dto.PathID = trip.PathID; 
            
            var list = new List<TripDTO.PassingTimeDTO>();
            foreach (var passingtime in trip.PassingTimes)
            {
                var passingTimeDTO = new TripDTO.PassingTimeDTO()
                {
                    NodeID = passingtime.NodeID,
                    TimeInstant = passingtime.TimeInstant
                };
                list.Add(passingTimeDTO);
            }


            dto.PassingTimes = list.OrderBy(p => p.TimeInstant).ToList();

            return dto;
        }
    }
}