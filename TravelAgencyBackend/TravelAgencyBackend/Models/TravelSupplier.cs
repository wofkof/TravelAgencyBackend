namespace TravelAgencyBackend.Models
{
    public class TravelSupplier
    {
        public int TravelSupplierId { get; set; }
        public string SupplierName { get; set; } = string.Empty;
        public string SupplierType { get; set; } = string.Empty;
        public string ContactName { get; set; } = string.Empty;
        public string ContactPhone { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
        public string? Note { get; set; }

        public ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
        public ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
        public ICollection<Attraction> Attractions { get; set; } = new List<Attraction>();
    }
}
