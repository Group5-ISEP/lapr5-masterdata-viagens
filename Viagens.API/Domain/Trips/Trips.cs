using lapr5_masterdata_viagens.Shared;
using lapr5_masterdata_viagens.Domain.Shared;
using lapr5_masterdata_viagens.Domain.Path;
using System.Collections.Generic;

namespace lapr5_masterdata_viagens.Domain.Trips
{
    public class Trip : Entity<TripId>, IAggregateRoot
    {
        public string PathID { get; private set; }
        public string LineID { get; private set; }

        public string Orientation { get; private set; }
        public List<PassingTime> PassingTimes { get; private set; }

        private Trip()
        {
            //FOR ORM
        }
        private Trip(string pathId, string lineId, string orientation, List<PassingTime> passingTimes, string id = null)
        {
            this.Id = new TripId(id);
            this.PathID = pathId;
            this.LineID = lineId;
            this.Orientation = orientation;
            this.PassingTimes = passingTimes;
        }

        public static Result<Trip> Create(int startTime, PathDTO pathDTO, TripDTO tripDTO = null)
        {
            if (pathDTO == null)
                return Result<Trip>.Fail("Path cant be null");

            var PathId = pathDTO.PathId;
            var LineId = pathDTO.LineId;
            var Orientation = pathDTO.Orientation;

            var passingTimesResult = GeneratePassingTimes(startTime, pathDTO);
            if (passingTimesResult.IsSuccess == false)
                return Result<Trip>.Fail(passingTimesResult.Error);

            var passingTimes = passingTimesResult.Value;

            if (tripDTO != null)
            {
                if (CheckValid(tripDTO, pathDTO, passingTimes) == false)
                    return Result<Trip>.Fail("Trip dto doesnt match the path dto and passing times generated");

                return Result<Trip>.Ok(new Trip(PathId, LineId, Orientation, passingTimes, tripDTO.Id));
            }

            return Result<Trip>.Ok(new Trip(PathId, LineId, Orientation, passingTimes));
        }

        private static Result<List<PassingTime>> GeneratePassingTimes(int startTime, PathDTO pathDTO)
        {
            var passingTimes = new List<PassingTime>();

            //passing time in start node
            var firstNode = pathDTO.Segments[0].StartNodeId;
            var passingTimeResult = PassingTime.Create(startTime, firstNode);
            if (passingTimeResult.IsSuccess == false)
                return Result<List<PassingTime>>.Fail(passingTimeResult.Error);
            passingTimes.Add(passingTimeResult.Value);

            //generate the other passing times
            var segments = pathDTO.Segments;
            var timeInstant = startTime;
            for (int position = 1; position <= segments.Count; position++)
            {
                var segment = segments.Find(seg => seg.Order == position);
                timeInstant += segment.Duration;
                passingTimeResult = PassingTime.Create(timeInstant, segment.EndNodeId);
                if (passingTimeResult.IsSuccess == false)
                    return Result<List<PassingTime>>.Fail(passingTimeResult.Error);
                passingTimes.Add(passingTimeResult.Value);
            }

            return Result<List<PassingTime>>.Ok(passingTimes);
        }

        private static bool CheckValid(TripDTO tripDTO, PathDTO pathDTO, List<PassingTime> passingTimes)
        {

            if (tripDTO.PathID != pathDTO.PathId)
                return false;
            if (tripDTO.LineID != pathDTO.LineId)
                return false;
            if (tripDTO.Orientation != pathDTO.Orientation)
                return false;
            foreach (var passingTimeDto in tripDTO.PassingTimes)
            {
                var passingTime = passingTimes.Find(pt => pt.NodeID == passingTimeDto.NodeID
                                                        && pt.TimeInstant == passingTimeDto.TimeInstant);

                if (passingTime == null)
                    return false;
            }

            return true;
        }

    }
}