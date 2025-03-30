using System.ComponentModel.DataAnnotations;

namespace TravelAgencyBackend.Models
{
    public enum SenderType
    {
        [Display(Name = "員工")]
        Employee,
        [Display(Name = "會員")]
        Member
    }
    public class Message
    {
        public int MessageId { get; set; }
        public int ChatRoomId { get; set; }
        public SenderType SenderType { get; set; }
        public int SenderId { get; set; } 
        public string Content { get; set; } = null!;
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }

        public ChatRoom ChatRoom { get; set; } = null!;
    }
}
