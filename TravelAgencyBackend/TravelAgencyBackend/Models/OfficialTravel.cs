using System.ComponentModel.DataAnnotations;

namespace TravelAgencyBackend.Models
{
    public enum TravelStatus
    {
        [Display(Name = "未上架")]
        Draft,
        [Display(Name = "上架中")]
        Published,
        [Display(Name = "已下架")]
        Unavailable
    }
    public class OfficialTravel
    {
        public int OfficialTravelId { get; set; }
        public int CreatedByEmployeeId { get; set; }
        public int RegionId { get; set; }
        public string Title { get; set; } = null!;
        public int ProjectYear { get; set; }
        public DateTime AvailableFrom { get; set; }
        public DateTime AvailableUntil { get; set; }
        public string Description { get; set; } = null!;
        public int Days { get; set; }
        public string CoverPath { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public TravelStatus Status { get; set; } = TravelStatus.Draft;

        public ICollection<OfficialTravelDetail> Details { get; set; } = new List<OfficialTravelDetail>();

        public Employee CreatedBy { get; set; } = null!;
        public Region Region { get; set; } = null!;
    }
}
