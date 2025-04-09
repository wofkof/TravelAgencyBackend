using System.ComponentModel.DataAnnotations;
using System.Data;

namespace TravelAgencyBackend.Models
{

    public enum EmployeeStatus
    {
        [Display(Name = "正常")]
        Active = 0,

        [Display(Name = "停權")]
        Suspended = 1,

        [Display(Name = "已刪除")]
        Deleted = 2 // ← 軟刪除專用
    }


    public enum GenderType
    {
        [Display(Name = "男性")]
        Male = 0, 

        [Display(Name = "女性")]
        Female = 1,

        [Display(Name = "其他")]
        Other = 2 
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

        // **新增的性別欄位**
        public GenderType Gender { get; set; } = GenderType.Male;

        // **新增的地址欄位**
        [MaxLength(255)]  // 限制最大長度為 255 字符
        public string Address { get; set; }

        public Role Role { get; set; } = null!;

        public ICollection<CustomTravel> ReviewedCustomTravels { get; set; } = new List<CustomTravel>();
        public ICollection<ChatRoom> ChatRooms { get; set; } = new List<ChatRoom>();
        public ICollection<Announcement> Announcements { get; set; } = new List<Announcement>();
        public ICollection<OfficialTravel> OfficialTravels { get; set; } = new List<OfficialTravel>();
    }
}
