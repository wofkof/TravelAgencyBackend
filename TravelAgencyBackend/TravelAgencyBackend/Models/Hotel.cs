namespace TravelAgencyBackend.Models
{
    public class Hotel
    {
        public int HotelId { get; set; }
        public int DistrictId { get; set; }
        public int TravelSupplierId { get; set; }
        public string HotelName { get; set; } = null!;

        public District District { get; set; } = null!;
        public TravelSupplier TravelSupplier { get; set; } = null!;
    }
}
