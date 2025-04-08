using System.ComponentModel.DataAnnotations;

namespace TravelAgencyBackend.Models
{
    public enum TravelActivityType
    {
        [Display(Name = "住宿")]
        Hotel,
        [Display(Name = "景點")]
        Attraction,
        [Display(Name = "餐廳")]
        Restaurant
    }
    public class OfficialTravelSchedule
    {
        public int OfficialTravelScheduleId { get; set; }

        public int OfficialTravelDetailId { get; set; }
        public int ItemId { get; set; }
        public TravelActivityType Category { get; set; }

        public int Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public DateTime? Date { get; set; }

        public string? Description { get; set; }
        public string? Note1 { get; set; }
        public string? Note2 { get; set; }

        public OfficialTravelDetail OfficialTravelDetail { get; set; } = null!;

        //public TravelSupplier TravelSupplier { get; set; } = null!;
    }
}
