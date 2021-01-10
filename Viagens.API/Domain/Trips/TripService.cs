using System.Threading.Tasks;
using System.Collections.Generic;
using lapr5_masterdata_viagens.Shared;
using lapr5_masterdata_viagens.Domain.Shared;
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
            var result = GenerateTrips(dto);
            if (result.IsSuccess == false)
                return Result<List<TripDTO>>.Fail(result.Error);

            List<TripDTO> SavedTrips = new List<TripDTO>();

            foreach (var trip in result.Value)
            {
                Trip TripSaved = await _repo.AddAsync(trip);
                SavedTrips.Add(TripMapper.ToDto(TripSaved));
            }

            await _unitOfWork.CommitAsync();

            return Result<List<TripDTO>>.Ok(SavedTrips);
        }

        private Result<List<Trip>> GenerateTrips(CreateTripsDTO dto)
        {

           /*  if (dto.Frequency < 1)
                return Result<List<Trip>>.Fail("Frequency must be more than 0");

            if (dto.NumberOfTrips < 1)
                return Result<List<Trip>>.Fail("Number of trips must be more than 0");

            var TripList = new List<Trip>();

            //Generate the trips of the To path
            for (int tripnumber = 0; tripnumber < dto.NumberOfTrips; tripnumber++)
            {
                var startTime = dto.StartTime + (tripnumber * dto.Frequency);

                var result = Trip.Create(startTime, dto.PathTo);
                if (result.IsSuccess == false)
                    return Result<List<Trip>>.Fail(result.Error);

                TripList.Add(result.Value);
            }

            //start time of the first From path trip
            var startTimeFromPath = dto.StartTime;
            foreach (var segment in dto.PathTo.Segments)
            {
                startTimeFromPath += segment.Duration;
            }

            //Generate the trips of the From path
            for (int tripnumber = 0; tripnumber < dto.NumberOfTrips; tripnumber++)
            {
                var startTime = startTimeFromPath + (tripnumber * dto.Frequency);

                var result = Trip.Create(startTime, dto.PathFrom);
                if (result.IsSuccess == false)
                    return Result<List<Trip>>.Fail(result.Error);

                TripList.Add(result.Value);
            }

            return Result<List<Trip>>.Ok(TripList); */

            return Result<List<Trip>>.Ok(new List<Trip>());
        }
    }
}