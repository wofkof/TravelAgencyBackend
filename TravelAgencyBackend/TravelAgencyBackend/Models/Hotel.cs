using System.ComponentModel.DataAnnotations;

namespace TravelAgencyBackend.Models
{
    public class Hotel
    {
        [Display(Name = "住宿編號")]
        public int HotelId { get; set; }
        [Display(Name = "區編號")]
        public int DistrictId { get; set; }
        [Display(Name = "供應商編號")]
        public int TravelSupplierId { get; set; }
        [Display(Name = "住宿名稱")]
        public string HotelName { get; set; } = null!;

        public District District { get; set; } = null!;
        public TravelSupplier TravelSupplier { get; set; } = null!;
    }
}
