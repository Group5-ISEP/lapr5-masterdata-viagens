using System.Threading.Tasks;
using System.Collections.Generic;
using lapr5_masterdata_viagens.Shared;
using lapr5_masterdata_viagens.Domain.Shared;
using lapr5_masterdata_viagens.Domain.Path;
using lapr5_masterdata_viagens.Infrastructure.MDRHttpClient;

namespace lapr5_masterdata_viagens.Domain.Trips
{
    public class TripService
    {
        private readonly ITripRepo _repo;
        private readonly IUnitOfWork _unitOfWork;

        private readonly MDRHttpClientInterface _client;

        public TripService(ITripRepo repo, IUnitOfWork unitOfWork, MDRHttpClientInterface client)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._client = client;
        }


        public async Task<Result<List<TripDTO>>> GetByLine(string lineId)
        {
            var result = await this._repo.GetByLine(lineId);

            var dtolist = new List<TripDTO>();

            foreach (var trip in result)
            {
                dtolist.Add(TripMapper.ToDto(trip));
            }

            return Result<List<TripDTO>>.Ok(dtolist);
        }

        public async Task<Result<List<TripDTO>>> CreateTrips(CreateTripsDTO dto)
        {
            if (dto.PathTo == null || dto.PathFrom == null)
                return Result<List<TripDTO>>.Fail("Path id cant be null");
            if (dto.Line == null)
                return Result<List<TripDTO>>.Fail("Line id cant be null");

            var fetchResult = await this._client.FetchPathsByLine(dto.Line);
            if (fetchResult.IsSuccess == false)
                return Result<List<TripDTO>>.Fail(fetchResult.Error);

            var pathDtoList = fetchResult.Value;
            var pathDtoTo = pathDtoList.Find(pathDto => pathDto.PathId == dto.PathTo);
            var pathDtoFrom = pathDtoList.Find(pathDto => pathDto.PathId == dto.PathFrom);
            if (pathDtoTo == null || pathDtoFrom == null)
                return Result<List<TripDTO>>.Fail("Path not found");


            var result = GenerateTrips(dto, pathDtoTo, pathDtoFrom);
            if (result.IsSuccess == false)
                return Result<List<TripDTO>>.Fail(result.Error);

            List<TripDTO> savedTrips = new List<TripDTO>();

            foreach (var trip in result.Value)
            {
                Trip tripSaved = await _repo.AddAsync(trip);
                savedTrips.Add(TripMapper.ToDto(tripSaved));
            }

            await _unitOfWork.CommitAsync();

            return Result<List<TripDTO>>.Ok(savedTrips);
        }

        private Result<List<Trip>> GenerateTrips(CreateTripsDTO dto, PathDTO pathTo, PathDTO pathFrom)
        {

            if (dto.Frequency < 1)
                return Result<List<Trip>>.Fail("Frequency cant be less than one");

            if (dto.NumberOfTrips < 1)
                return Result<List<Trip>>.Fail("Number of trips cant be less than one");

            var TripList = new List<Trip>();

            //Generate the trips of the To path
            for (int tripnumber = 0; tripnumber < dto.NumberOfTrips; tripnumber++)
            {
                var startTime = dto.StartTime + (tripnumber * dto.Frequency);

                var result = Trip.Create(startTime, pathTo);
                if (result.IsSuccess == false)
                    return Result<List<Trip>>.Fail(result.Error);

                TripList.Add(result.Value);
            }

            //start time of the first From path trip
            var startTimeFromPath = dto.StartTime;
            foreach (var segment in pathTo.Segments)
            {
                startTimeFromPath += segment.Duration;
            }

            //Generate the trips of the From path
            for (int tripnumber = 0; tripnumber < dto.NumberOfTrips; tripnumber++)
            {
                var startTime = startTimeFromPath + (tripnumber * dto.Frequency);

                var result = Trip.Create(startTime, pathFrom);
                if (result.IsSuccess == false)
                    return Result<List<Trip>>.Fail(result.Error);

                TripList.Add(result.Value);
            }

            return Result<List<Trip>>.Ok(TripList);
        }
    }
}