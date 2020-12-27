using System.Collections.Generic;

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
            dto.PassingTimes = new List<TripDTO.PassingTimeDTO>();

            foreach (var passingtime in trip.PassingTimes)
            {
                var passingTimeDTO = new TripDTO.PassingTimeDTO()
                {
                    NodeID = passingtime.NodeID,
                    TimeInstant = passingtime.TimeInstant
                };
                dto.PassingTimes.Add(passingTimeDTO);
            }

            return dto;
        }
    }
}