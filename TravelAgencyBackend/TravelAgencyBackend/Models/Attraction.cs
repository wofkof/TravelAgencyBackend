namespace TravelAgencyBackend.Models
{
    public class Attraction
    {
        public int AttractionId { get; set; }
        public int DistrictId { get; set; }
        public int TravelSupplierId { get; set; }
        public string ScenicSpotName { get; set; } = null!;

        public District District { get; set; } = null!;
        public TravelSupplier TravelSupplier { get; set; } = null!;
    }
}
