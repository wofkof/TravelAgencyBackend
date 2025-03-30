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
        public int MemberId { get; set; }
        public string Account { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public MemberStatus Status { get; set; } = MemberStatus.Active;
        public string? Note { get; set; }

        public ICollection<CustomTravel> CustomTravels { get; set; } = new List<CustomTravel>();
        public ICollection<Cart> Carts { get; set; } = new List<Cart>();
        public ICollection<Participant> Participants { get; set; } = new List<Participant>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ChatRoom? ChatRoom { get; set; }
    }
}
