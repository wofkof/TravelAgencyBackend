using System.ComponentModel.DataAnnotations;

namespace TravelAgencyBackend.Models
{
    public class Restaurant
    {
        [Display(Name = "餐廳編號")]
        public int RestaurantId { get; set; }
        [Display(Name = "區編號")]
        public int DistrictId { get; set; }
        [Display(Name = "供應商編號")]
        public int TravelSupplierId { get; set; }
        [Display(Name = "餐廳名稱")]
        public string RestaurantName { get; set; } = null!;

        public District District { get; set; } = null!;
        public TravelSupplier TravelSupplier { get; set; } = null!;
    }
}
