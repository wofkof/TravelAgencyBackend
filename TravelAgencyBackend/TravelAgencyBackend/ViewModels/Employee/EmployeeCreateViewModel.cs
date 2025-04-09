using System.ComponentModel.DataAnnotations;
using TravelAgencyBackend.Models;

namespace TravelAgencyBackend.ViewModels.Employee
{
    public class EmployeeCreateViewModel
    {
        [Display(Name = "姓名")]
        [Required]
        public string Name { get; set; } = null!;

        [Display(Name = "密碼")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Display(Name = "聯絡信箱")]
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Display(Name = "電話")]
        [Required]
        [RegularExpression(@"^09\d{8}$", ErrorMessage = "手機號碼格式不正確")]
        [StringLength(10, MinimumLength = 10)]
        public string Phone { get; set; } = null!;

        [Display(Name = "生日")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "入職日期")]
        public DateTime HireDate { get; set; }

        [Display(Name = "性別")]
        [Required(ErrorMessage = "請選擇性別")]
        public GenderType? Gender { get; set; }

        [Display(Name = "狀態")]
        [Required(ErrorMessage = "請選擇員工狀態")]
        public EmployeeStatus? Status { get; set; }

        [Display(Name = "地址")]
        public string? Address { get; set; }

        [Display(Name = "備註")]
        public string? Note { get; set; }

        [Required(ErrorMessage = "請選擇員工職位")]
        [Display(Name = "職位")]
        public int? RoleId { get; set; }

        [Display(Name = "大頭貼")]
        public IFormFile? Photo { get; set; }  // 上傳圖檔


    }
}
