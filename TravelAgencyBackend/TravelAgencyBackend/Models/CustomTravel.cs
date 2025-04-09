using System.ComponentModel.DataAnnotations;

namespace TravelAgencyBackend.Models
{
    public enum CustomTravelStatus
    {
        [Display(Name = "待處理")]
        Pending,
        [Display(Name = "已核准")]
        Approved,
        [Display(Name = "已拒絕")]
        Rejected,
        [Display(Name = "已取消")]
        Cancelled
    }
    public class CustomTravel
    {
        [Display(Name = "客製化行程編號")]
        public int CustomTravelId { get; set; }
        [Display(Name = "會員編號")]
        public int MemberId { get; set; }
        [Display(Name = "審核員工編號")]
        public int? ReviewEmployeeId { get; set; }
        [Display(Name = "建立日期")]
        public DateTime CreatedAt { get; set; }
        [Display(Name = "修改日期")]
        public DateTime UpdatedAt { get; set; }
        [Display(Name = "出發日期")]
        public DateTime DepartureDate { get; set; }
        [Display(Name = "結束日期")]
        public DateTime EndDate { get; set; }
        [Display(Name = "天數")]
        public int Days { get; set; }
        [Display(Name = "人數")]
        public int People { get; set; }
        [Display(Name = "總金額")]
        public decimal TotalAmount { get; set; }
        [Display(Name = "狀態")]
        public CustomTravelStatus Status { get; set; } = CustomTravelStatus.Pending;
        [Display(Name = "備註")]
        public string? Note { get; set; }

        public ICollection<Content> Contents { get; set; } = new List<Content>();

        public Member Member { get; set; } = null!;
        public Employee? ReviewEmployee { get; set; }
    }
}
