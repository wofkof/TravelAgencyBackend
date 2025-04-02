using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TravelAgencyBackend.Models
{
    public enum MemberStatus
    {
        [Display(Name = "正常")]
        Active,
        [Display(Name = "停權")]
        Suspended
    }
    public class Member
    {
        [DisplayName("會員編號")]
        public int MemberId { get; set; }
        [Required(ErrorMessage = "請輸入帳號")]
        [DisplayName("帳號")]
        public string Account { get; set; } = null!;
        [Required(ErrorMessage = "請輸入密碼")]
        [DisplayName("密碼")]
        public string Password { get; set; } = null!;
        [Required(ErrorMessage = "請輸入姓名")]
        [DisplayName("姓名")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "請輸入信箱")]
        [DisplayName("信箱")]
        [EmailAddress(ErrorMessage = "格式不正確")]
        public string Email { get; set; } = null!;
        [RegularExpression(@"^09\d{8}$", ErrorMessage = "手機號碼格式不正確")]
        [StringLength(10,MinimumLength = 10)]
        [Required(ErrorMessage = "請輸入手機")]
        [DisplayName("手機")]
        public string Phone { get; set; } = null!;
        [DisplayName("註冊時間")]
        public DateTime CreatedAt { get; set; }
        [DisplayName("更新時間")]
        public DateTime? UpdatedAt { get; set; }
        [DisplayName("狀態")]
        public MemberStatus Status { get; set; } = MemberStatus.Active;
        [DisplayName("備註")]
        public string? Note { get; set; }

        public ICollection<CustomTravel> CustomTravels { get; set; } = new List<CustomTravel>();
        public ICollection<Cart> Carts { get; set; } = new List<Cart>();
        public ICollection<Participant> Participants { get; set; } = new List<Participant>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ChatRoom? ChatRoom { get; set; }
    }
}
