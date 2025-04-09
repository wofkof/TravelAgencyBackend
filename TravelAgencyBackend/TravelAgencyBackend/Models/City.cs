using System.ComponentModel.DataAnnotations;

namespace TravelAgencyBackend.Models
{
    public class City
    {
        [Display(Name = "縣市編號")]
        public int CityId { get; set; }
        [Display(Name = "縣市名稱")]
        public string CityName { get; set; } = null!;

        public ICollection<District> Districts { get; set; } = new List<District>();

    }
}
