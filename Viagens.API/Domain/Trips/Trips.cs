using System;
using lapr5_masterdata_viagens.Shared;
using System.Globalization;
using System.Text.RegularExpressions;
using lapr5_masterdata_viagens.Domain.Shared;
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
        private Trip(string pathId, string lineId, string orientation, List<PassingTime> passingTimes)
        {
            this.Id = new TripId(Guid.NewGuid().ToString());
            this.PathID = pathId;
            this.LineID = lineId;
            this.Orientation = orientation;
            this.PassingTimes = passingTimes;
        }

        public static Result<Trip> Create(int startTime, PathDTO pathDTO)
        {

            var PathId = pathDTO.PathId;
            var LineId = pathDTO.LineId;
            var Orientation = pathDTO.Orientation;

            if (startTime < 0)
                return Result<Trip>.Fail("Trip start time must be greater than 0");

            var passingTimes = new List<PassingTime>();

            //passing time in start node
            var firstNode = pathDTO.Segments[0].StartNodeId;
            passingTimes.Add(new PassingTime(startTime, firstNode));

            //generate the other passing times
            var segments = pathDTO.Segments;
            var timeInstant = startTime;
            for (int position = 1; position < segments.Count; position++)
            {
                var segment = segments.Find(seg => seg.Order == position);
                timeInstant += segment.Duration;
                passingTimes.Add(new PassingTime(timeInstant, segment.EndNodeId));
            }

            Trip Trip = new Trip(PathId, LineId, Orientation, passingTimes);
            return Result<Trip>.Ok(Trip);
        }
    }
}