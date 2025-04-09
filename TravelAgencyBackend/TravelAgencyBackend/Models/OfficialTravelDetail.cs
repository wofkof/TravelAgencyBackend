using System.ComponentModel.DataAnnotations;

namespace TravelAgencyBackend.Models
{
    public enum GroupStatus
    {
        [Display(Name = "尚未成行")]
        Pending,
        [Display(Name = "已成行")]
        Confirmed,
        [Display(Name = "取消")]
        Canceled
    }
    public class OfficialTravelDetail
    {
        public int OfficialTravelDetailId { get; set; }
        [Display(Name = "專案名稱")]
        public int OfficialTravelId { get; set; }
        [Display(Name = "航班編號")]
        public int FlightId { get; set; }
        [Display(Name = "去程日期")]
        public DateTime DepartureDate { get; set; }
        [Display(Name = "回程日期")]
        public DateTime ReturnDate { get; set; }
        [Display(Name = "價格")]
        public decimal Price { get; set; }
        [Display(Name = "總席次")]
        public int Seats { get; set; }
        [Display(Name = "賣出席次")]
        public int SoldSeats { get; set; }
        [Display(Name = "最低出團人數")]
        public int MinimumGroupSize { get; set; }
        [Display(Name = "下單作業截止日期")]
        public DateTime BookingDeadline { get; set; }
        [Display(Name = "出團狀態")]
        public GroupStatus GroupStatus { get; set; } = GroupStatus.Pending;
        [Display(Name = "創建日期")]
        public DateTime CreatedAt { get; set; }
        [Display(Name = "最新更新")]
        public DateTime? UpdatedAt { get; set; }

        public ICollection<OfficialTravelSchedule> Schedules { get; set; } = new List<OfficialTravelSchedule>();
        [Display(Name = "所屬專案")]
        public OfficialTravel OfficialTravel { get; set; } = null!;
        [Display(Name = "預定航班")]
        public Flight Flight { get; set; } = null!;
    }
}
