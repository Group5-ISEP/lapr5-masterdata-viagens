namespace lapr5_masterdata_viagens.Domain.Trips
{
    public class CreateTripsDTO
    {

        public int Frequency { get; set; }
        public int NumberOfTrips { get; set; }
        public int StartTime { get; set; }
        public string Line { get; set; }
        public string PathTo { get; set; }
        public string PathFrom { get; set; }
    }
}