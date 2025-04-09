using System.ComponentModel.DataAnnotations;

namespace TravelAgencyBackend.Models
{
    public class Transportation
    {
        [Display(Name = "交通方式編號")]
        public int TransportId { get; set; }
        [Display(Name = "交通方式名稱")]
        public string TransportMethod { get; set; } = null!;
    }
}
