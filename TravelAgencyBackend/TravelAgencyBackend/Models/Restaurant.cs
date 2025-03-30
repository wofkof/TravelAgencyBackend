namespace TravelAgencyBackend.Models
{
    public class Restaurant
    {
        public int RestaurantId { get; set; }
        public int DistrictId { get; set; }
        public int TravelSupplierId { get; set; }
        public string RestaurantName { get; set; } = null!;

        public District District { get; set; } = null!;
        public TravelSupplier TravelSupplier { get; set; } = null!;
    }
}
