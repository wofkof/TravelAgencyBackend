namespace TravelAgencyBackend.Models
{
    public class Flight
    {
        public int FlightId { get; set; }
        public string AirlineCode { get; set; } = string.Empty;
        public string AirlineName { get; set; } = string.Empty;
        public string DepartureAirportCode { get; set; } = string.Empty;
        public string DepartureAirportName { get; set; } = string.Empty;
        public string ArrivalAirportCode { get; set; } = string.Empty;
        public string ArrivalAirportName { get; set; } = string.Empty;
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string Status { get; set; } = string.Empty;
        public string AircraftType { get; set; } = string.Empty;
        public string FlightUid { get; set; }   = string.Empty;
        public DateTime SyncedAt { get; set; }

        public ICollection<OfficialTravelDetail> OfficialTravelDetails { get; set; } = new List<OfficialTravelDetail>();
    }
}
