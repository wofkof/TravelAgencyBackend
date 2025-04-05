using System.ComponentModel.DataAnnotations;
using TravelAgencyBackend.Models;

namespace TravelAgencyBackend.ViewModels
{
    public class EmployeeEditViewModel
    {
        public int EmployeeId { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Phone { get; set; } = null!;

        public DateTime BirthDate { get; set; }

        public DateTime HireDate { get; set; }

        [Required]
        public GenderType Gender { get; set; }

        [Required]
        public EmployeeStatus Status { get; set; }

        public string? Address { get; set; }

        public string? Note { get; set; }

        [Required]
        public int RoleId { get; set; }

        // 🔐 密碼為 null 代表不修改
        public string? Password { get; set; }
    }
}
