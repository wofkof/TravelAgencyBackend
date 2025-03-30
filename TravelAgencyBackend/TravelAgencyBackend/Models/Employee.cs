using System.ComponentModel.DataAnnotations;
using System.Data;

namespace TravelAgencyBackend.Models
{
    public enum EmployeeStatus
    {
        [Display(Name = "正常")]
        Active,
        [Display(Name = "停權")]
        Suspended
    }
    public class Employee
    {
        public int EmployeeId { get; set; }
        public int RoleId { get; set; }
        public string Name { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public DateTime HireDate { get; set; }
        public EmployeeStatus Status { get; set; } = EmployeeStatus.Active;
        public string? Note { get; set; }

        public Role Role { get; set; } = null!;

        public ICollection<CustomTravel> ReviewedCustomTravels { get; set; } = new List<CustomTravel>();
        public ICollection<ChatRoom> ChatRooms { get; set; } = new List<ChatRoom>();
        public ICollection<Announcement> Announcements { get; set; } = new List<Announcement>();
        public ICollection<OfficialTravel> OfficialTravels { get; set; } = new List<OfficialTravel>();
    }
}
