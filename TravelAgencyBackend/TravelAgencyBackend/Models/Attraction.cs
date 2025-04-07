using System.ComponentModel.DataAnnotations;

namespace TravelAgencyBackend.Models
{
    public class Attraction
    {
        [Display(Name = "景點編號")]
        public int AttractionId { get; set; }
        [Display(Name = "區編號")]
        public int DistrictId { get; set; }
        [Display(Name = "供應商編號")]
        public int TravelSupplierId { get; set; }
        [Display(Name = "景點名稱")]
        public string ScenicSpotName { get; set; } = null!;

        public District District { get; set; } = null!;
        public TravelSupplier TravelSupplier { get; set; } = null!;
    }
}
