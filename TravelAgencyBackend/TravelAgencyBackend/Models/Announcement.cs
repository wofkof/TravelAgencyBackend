using System.ComponentModel.DataAnnotations;

namespace TravelAgencyBackend.Models
{
    public enum AnnouncementStatus
    {
        [Display(Name = "已公佈")]
        Announce,
        [Display(Name = "未公佈")]
        Unannounced,
        [Display(Name = "已刪除")]
        Deleted
    }
    public class Announcement
    {
        public int AnnouncementId { get; set; }
        public int EmployeeId { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime SentAt { get; set; }
        public AnnouncementStatus Status { get; set; } = AnnouncementStatus.Unannounced;

        public Employee Employee { get; set; } = null!;
    }
}
