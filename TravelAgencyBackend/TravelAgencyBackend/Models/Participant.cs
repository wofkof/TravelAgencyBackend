using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TravelAgencyBackend.Models
{
    public enum GenderType 
    {
        [Display(Name ="男性")]
        Male,
        [Display(Name = "女性")]
        Female,
        [Display(Name = "其他")]
        Other
    }
    public class Participant
    {
        [DisplayName("參與人編號")]
        public int ParticipantId { get; set; }
        [DisplayName("會員編號")]
        public int MemberId { get; set; }
        [Required(ErrorMessage = "請輸入參與人姓名")]
        [DisplayName("參與人姓名")]
        public string Name { get; set; } = null!;
        [RegularExpression(@"^09\d{8}$", ErrorMessage = "手機號碼格式不正確")]
        [StringLength(10, MinimumLength = 10)]
        [Required(ErrorMessage = "請輸入手機")]
        [DisplayName("手機")]
        public string Phone { get; set; } = null!;
        [RegularExpression(@"^[A-Z]{1}\d{9}$", ErrorMessage = "身分證格式不正確")]
        [StringLength(10, MinimumLength = 10)]
        [Required(ErrorMessage = "請輸入身分證")]
        [DisplayName("身分證")]
        public string IdNumber { get; set; } = null!;
        [DisplayName("性別")]
        public GenderType Gender { get; set; } = GenderType.Other;
        [Required(ErrorMessage = "請輸入生日")]
        [DisplayName("生日")]
        public DateTime? BirthDate { get; set; }
        [Required(ErrorMessage = "請輸入參與人英文姓名")]
        [DisplayName("英文姓名")]
        public string EnglishName { get; set; } = null!;
        [RegularExpression(@"^[A-Z]{1}\d{8}$", ErrorMessage = "護照號碼格式不正確")]
        [StringLength(9, MinimumLength = 9)]
        [Required(ErrorMessage = "請輸入參與人護照號碼")]
        [DisplayName("護照號碼")]
        public string PassportNumber { get; set; } = null!;
        [Required(ErrorMessage = "請選擇發照地")]
        [DisplayName("發照地")]
        public string IssuedPlace { get; set; } = null!;
        [Required(ErrorMessage = "請輸入護照效期")]
        [DisplayName("護照效期起日")]
        public DateTime? PassportIssueDate { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
        

        [ValidateNever]
        public Member Member { get; set; } = null!;
    }
}
