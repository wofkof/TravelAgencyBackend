using System.ComponentModel.DataAnnotations;

namespace TravelAgencyBackend.Models
{
    public class Flight
    {
        public int FlightId { get; set; }
        [Display(Name = "航線代號")]
        public string AirlineCode { get; set; } = string.Empty;
        [Display(Name = "航線名稱")]
        public string AirlineName { get; set; } = string.Empty;
        [Display(Name = "起飛機場代號")]
        public string DepartureAirportCode { get; set; } = string.Empty;
        [Display(Name = "起飛機場名稱")]
        public string DepartureAirportName { get; set; } = string.Empty;
        [Display(Name = "降落機場代號")]
        public string ArrivalAirportCode { get; set; } = string.Empty;
        [Display(Name = "降落機場名稱")]
        public string ArrivalAirportName { get; set; } = string.Empty;
        [Display(Name = "起飛時間")]
        public DateTime DepartureTime { get; set; }
        [Display(Name = "降落時間")]
        public DateTime ArrivalTime { get; set; }
        [Display(Name = "狀態")]
        public string Status { get; set; } = string.Empty;
        [Display(Name = "型號")]
        public string AircraftType { get; set; } = string.Empty;
        [Display(Name = "飛機代號")]
        public string FlightUid { get; set; }   = string.Empty;
        [Display(Name = "資料同步時間")]
        public DateTime SyncedAt { get; set; }

        public ICollection<OfficialTravelDetail> OfficialTravelDetails { get; set; } = new List<OfficialTravelDetail>();
    }
}
