using System.ComponentModel.DataAnnotations;

namespace TravelAgencyBackend.Models
{
    public enum GroupStatus
    {
        [Display(Name = "尚未成行")]
        Pending,
        [Display(Name = "已成行")]
        Confirmed,
        [Display(Name = "取消")]
        Canceled
    }
    public class OfficialTravelDetail
    {
        public int OfficialTravelDetailId { get; set; }
        public int OfficialTravelId { get; set; }
        public int FlightId { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal Price { get; set; }
        public int Seats { get; set; }
        public int SoldSeats { get; set; }
        public int MinimumGroupSize { get; set; }
        public DateTime BookingDeadline { get; set; }
        public GroupStatus GroupStatus { get; set; } = GroupStatus.Pending;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public ICollection<OfficialTravelSchedule> Schedules { get; set; } = new List<OfficialTravelSchedule>();
       
        public OfficialTravel OfficialTravel { get; set; } = null!;
        public Flight Flight { get; set; } = null!;
    }
}
