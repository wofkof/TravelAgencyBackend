using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TravelAgencyBackend.Models
{
    public enum GenderTypes 
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
        public int ParticipantId { get; set; }
        
        public int MemberId { get; set; }
        
        public string Name { get; set; } = null!;
        
        public string Phone { get; set; } = null!;
        
        public string IdNumber { get; set; } = null!;
        
        public GenderTypes Gender { get; set; } = GenderTypes.Other;
        
        public DateTime? BirthDate { get; set; }
        
        public string EnglishName { get; set; } = null!;
        
        public string PassportNumber { get; set; } = null!;
        
        public string IssuedPlace { get; set; } = null!;
        
        public DateTime? PassportIssueDate { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
        
        [ValidateNever]
        public Member Member { get; set; } = null!;
    }
}
