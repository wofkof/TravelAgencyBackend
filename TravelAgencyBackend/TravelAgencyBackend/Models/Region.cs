using System.ComponentModel.DataAnnotations;

namespace TravelAgencyBackend.Models
{
    public class Region
    {
        public int RegionId { get; set; }
        public string Country { get; set; } = null!;
        public string Name { get; set; } = null!;

        public ICollection<OfficialTravel> OfficialTravels { get; set; } = new List<OfficialTravel>();
    }
}

