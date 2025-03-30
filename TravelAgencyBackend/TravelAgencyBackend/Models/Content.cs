using System.ComponentModel.DataAnnotations;

namespace TravelAgencyBackend.Models
{
    public enum Category
    {
        [Display(Name = "住宿")]
        Hotel,
        [Display(Name = "交通")]
        Activity,
        [Display(Name = "餐廳")]
        Restaurant,
        [Display(Name = "景點")]
        Attraction    
    }
    public class Content
    {
        public int ContentId { get; set; }
        public int CustomTravelId { get; set; }
        public int ItemId { get; set; } 
        public Category Category { get; set; } 
        public int Day { get; set; }
        public TimeSpan Time { get; set; }
        public string? HotelName { get; set; } 

        public CustomTravel CustomTravel { get; set; } = null!;
    }
}
