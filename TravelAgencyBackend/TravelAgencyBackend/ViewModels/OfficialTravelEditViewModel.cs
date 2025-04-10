using System.ComponentModel.DataAnnotations;

namespace TravelAgencyBackend.ViewModels
{
    public class OfficialTravelEditViewModel
    {
        public int OfficialTravelId { get; set; }
        public int CreatedByEmployeeId { get; set; }
        public int RegionId { get; set; }

        [Required]
        public string Title { get; set; }

        public int ProjectYear { get; set; }

        [DataType(DataType.Date)]
        public DateTime AvailableFrom { get; set; }

        [DataType(DataType.Date)]
        public DateTime AvailableUntil { get; set; }

        public string? Description { get; set; }

        public int Days { get; set; }

        public string? CoverPath { get; set; }

        public DateTime CreatedAt { get; set; }

        public TravelStatus Status { get; set; }

        public IFormFile? Cover { get; set; }
    }
}
