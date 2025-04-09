using System.ComponentModel.DataAnnotations;

namespace TravelAgencyBackend.Models
{
    public class TravelSupplier
    {
        public int TravelSupplierId { get; set; }

        [Display(Name ="供應商")]
        public string SupplierName { get; set; } = string.Empty;
        [Display(Name = "類型")]
        public string SupplierType { get; set; } = string.Empty;
        [Display(Name = "負責人姓名")]
        public string ContactName { get; set; } = string.Empty;
        [Display(Name = "電話")]
        public string ContactPhone { get; set; } = string.Empty;
        [Display(Name = "電子郵件")]
        public string ContactEmail { get; set; } = string.Empty;
        [Display(Name = "備註")]
        public string? Note { get; set; }

        public ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
        public ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
        public ICollection<Attraction> Attractions { get; set; } = new List<Attraction>();
    }
}
