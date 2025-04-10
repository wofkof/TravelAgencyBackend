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
        [Display(Name = "內容編號")]
        public int ContentId { get; set; }
        [Display(Name = "客製化行程編號")]
        public int CustomTravelId { get; set; }
        [Display(Name = "行程")]
        public int ItemId { get; set; }
        [Display(Name = "行程分類名稱")]
        public Category Category { get; set; }
        [Display(Name = "日程")]
        public int Day { get; set; }
        [Display(Name = "時間")]
        public TimeSpan Time { get; set; }
        [Display(Name = "住宿名稱(備註)")]
        public string? HotelName { get; set; } 

        public CustomTravel CustomTravel { get; set; } = null!;
    }
}
