namespace TravelAgencyBackend.Models
{
    public class City
    {
        public int CityId { get; set; }
        public string CityName { get; set; } = null!;

        public ICollection<District> Districts { get; set; } = new List<District>();

    }
}
