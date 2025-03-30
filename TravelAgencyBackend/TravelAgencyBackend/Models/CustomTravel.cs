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
        public int CustomTravelId { get; set; }
        public int MemberId { get; set; }
        public int? ReviewEmployeeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Days { get; set; }
        public int People { get; set; }
        public decimal TotalAmount { get; set; }
        public CustomTravelStatus Status { get; set; } = CustomTravelStatus.Pending;
        public string? Note { get; set; }

        public ICollection<Content> Contents { get; set; } = new List<Content>();

        public Member Member { get; set; } = null!;
        public Employee? ReviewEmployee { get; set; }
    }
}
