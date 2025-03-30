namespace TravelAgencyBackend.Models
{
    public class District
    {
        public int DistrictId { get; set; }
        public int CityId { get; set; }
        public string DistrictName { get; set; } = null!;


        public ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
        public ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
        public ICollection<Attraction> Attractions { get; set; } = new List<Attraction>();
        public City City { get; set; } = null!;
    }
}
