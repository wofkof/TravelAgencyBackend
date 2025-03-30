using System.ComponentModel.DataAnnotations;

namespace TravelAgencyBackend.Models
{
    public enum ChatStatus
    {
        [Display(Name = "已開啟")]
        Opened,
        [Display(Name = "已關閉")]
        Closed 
    }
    public class ChatRoom
    {
        public int ChatRoomId { get; set; }
        public int EmployeeId { get; set; }
        public int MemberId { get; set; }
        public DateTime CreatedAt { get; set; }
        public ChatStatus Status { get; set; } = ChatStatus.Opened;

        public ICollection<Message> Messages { get; set; } = new List<Message>();

        public Member Member { get; set; } = null!;
        public Employee Employee { get; set; } = null!;
    }
}
