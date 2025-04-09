using System.ComponentModel.DataAnnotations;
using TravelAgencyBackend.Models;

namespace TravelAgencyBackend.ViewModels.Employee
{
    public class EmployeeEditViewModel
    {
        public int EmployeeId { get; set; }

        [Display(Name = "姓名")]
        [Required]
        public string Name { get; set; } = null!;

        [Display(Name = "聯絡信箱")]
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Display(Name = "電話")]
        [Required]
        public string Phone { get; set; } = null!;

        [Display(Name = "生日")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "入職日期")]
        public DateTime HireDate { get; set; }

        [Display(Name = "性別")]
        [Required]
        public GenderType Gender { get; set; }

        [Display(Name = "狀態")]
        [Required]
        public EmployeeStatus Status { get; set; }

        [Display(Name = "地址")]
        public string? Address { get; set; }

        [Display(Name = "備註")]
        public string? Note { get; set; }

        [Display(Name = "職位名稱")]
        [Required]
        public int RoleId { get; set; }

        // 🔐 密碼為 null 代表不修改
        public string? Password { get; set; }
    }
}
