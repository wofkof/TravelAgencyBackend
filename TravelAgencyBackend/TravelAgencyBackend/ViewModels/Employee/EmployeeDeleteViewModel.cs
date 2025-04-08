using System.ComponentModel.DataAnnotations;

namespace TravelAgencyBackend.ViewModels.Employee
{
    public class EmployeeDeleteViewModel
    {
        public int EmployeeId { get; set; }

        [Display(Name = "姓名")]
        public string Name { get; set; } = null!;

        [Display(Name = "職位")]
        public string RoleName { get; set; } = null!;

        [Display(Name = "電話")]
        public string Phone { get; set; } = null!;

        [Display(Name = "聯絡信箱")]
        public string Email { get; set; } = null!;

        [Display(Name = "備註")]
        public string? Note { get; set; }
    }
}
