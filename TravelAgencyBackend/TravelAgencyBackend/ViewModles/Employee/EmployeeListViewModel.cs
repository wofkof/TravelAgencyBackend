using System.ComponentModel.DataAnnotations;
using TravelAgencyBackend.Models;

namespace TravelAgencyBackend.ViewModels.Employee
{
    public class EmployeeListViewModel
    {
        public int EmployeeId { get; set; }

        [Display(Name = "姓名")]
        public string Name { get; set; } = null!;

        [Display(Name = "性別")]
        public GenderType Gender { get; set; }

        [Display(Name = "生日")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "電話")]
        public string Phone { get; set; } = null!;

        [Display(Name = "聯絡信箱")]
        public string Email { get; set; } = null!;

        [Display(Name = "地址")]
        public string? Address { get; set; }

        [Display(Name = "入職日期")]
        public DateTime HireDate { get; set; }

        [Display(Name = "狀態")]
        public EmployeeStatus Status { get; set; }

        [Display(Name = "備註")]
        public string? Note { get; set; }

        [Display(Name = "職位")]
        public string RoleName { get; set; } = null!;
    }
}
