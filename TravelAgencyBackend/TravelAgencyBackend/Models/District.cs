using System.ComponentModel.DataAnnotations;

namespace TravelAgencyBackend.Models
{
    public class District
    {
        [Display(Name = "區編號")]
        public int DistrictId { get; set; }
        [Display(Name = "縣市編號")]
        public int CityId { get; set; }
        [Display(Name = "區名稱")]
        public string DistrictName { get; set; } = null!;


        public ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
        public ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
        public ICollection<Attraction> Attractions { get; set; } = new List<Attraction>();
        public City City { get; set; } = null!;
    }
}
