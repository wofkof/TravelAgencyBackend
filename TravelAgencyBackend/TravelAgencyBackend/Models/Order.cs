using System.ComponentModel.DataAnnotations;

namespace TravelAgencyBackend.Models
{
    public enum OrderStatus
    {
        [Display(Name = "待付款")]
        Pending,
        [Display(Name = "已付款")]
        Paid,
        [Display(Name = "已取消")]
        Cancelled
    }
    public enum OrderCategory
    {
        [Display(Name = "官方旅遊行程細節")]
        OfficialTravelDetail,
        [Display(Name = "客製化行程")]
        CustomTravel
    }
    public enum PaymentMethod
    {
        [Display(Name = "信用卡")]
        CreditCard,
        [Display(Name = "銀行轉帳")]
        BankTransfer,
        [Display(Name = "現金")]
        Cash 
    }
    public class Order
    {
        public int OrderId { get; set; }
        public int MemberId { get; set; }
        public int ItemId { get; set; } 
        public OrderCategory Category { get; set; }
        public int ParticipantId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ParticipantsCount { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? Note { get; set; }

        public ICollection<TravelRecord> TravelRecords { get; set; } = new List<TravelRecord>();

        public Member Member { get; set; } = null!;
        public Participant Participant { get; set; } = null!;
        public CustomTravel? CustomTravel { get; set; }
        public OfficialTravelDetail? OfficialTravelDetail { get; set; }
    }
}
