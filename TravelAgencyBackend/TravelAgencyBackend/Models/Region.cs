using System.ComponentModel.DataAnnotations;

namespace TravelAgencyBackend.Models
{
    public class Region
    {
        public int RegionId { get; set; }
        [Display(Name = "國家")]
        public string Country { get; set; } = null!;
        [Display(Name = "地區")]
        public string Name { get; set; } = null!;

        public ICollection<OfficialTravel> OfficialTravels { get; set; } = new List<OfficialTravel>();
    }
}

