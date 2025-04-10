using System.ComponentModel.DataAnnotations;

namespace TravelAgencyBackend.Models
{
    public enum TravelActivityType
    {
        [Display(Name = "住宿")]
        Hotel,
        [Display(Name = "景點")]
        Attraction,
        [Display(Name = "餐廳")]
        Restaurant
    }
    public class OfficialTravelSchedule
    {
        public int OfficialTravelScheduleId { get; set; }
        [Display(Name = "行程編號")]
        public int OfficialTravelDetailId { get; set; }
        [Display(Name = "活動編號")]
        public int ItemId { get; set; }
        [Display(Name = "活動類型")]
        public TravelActivityType Category { get; set; }
        [Display(Name = "天次")]
        public int Day { get; set; }
        [Display(Name = "活動開始時間")]
        public TimeSpan StartTime { get; set; }
        [Display(Name = "日期")]
        public DateTime? Date { get; set; }
        [Display(Name = "行程描述")]
        public string? Description { get; set; }
        [Display(Name = "備註1")]
        public string? Note1 { get; set; }
        [Display(Name = "備註2")]
        public string? Note2 { get; set; }
        [Display(Name = "所屬行程")]
        public OfficialTravelDetail? OfficialTravelDetail { get; set; }
    }
}
