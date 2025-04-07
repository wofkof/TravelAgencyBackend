using System.ComponentModel.DataAnnotations;

namespace TravelAgencyBackend.ViewModels

{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "請輸入電話")]
        [RegularExpression(@"^09\d{8}$", ErrorMessage = "請輸入正確的手機號碼")]
        [Display(Name = "電話")]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "請輸入密碼")]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string Password { get; set; } = null!;
    }
}
