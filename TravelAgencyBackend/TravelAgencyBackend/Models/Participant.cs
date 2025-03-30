namespace TravelAgencyBackend.Models
{
    public class Participant
    {
        public int ParticipantId { get; set; }
        public int MemberId { get; set; }
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string IdNumber { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string EnglishName { get; set; } = null!;
        public string PassportNumber { get; set; } = null!;
        public string IssuedPlace { get; set; } = null!;
        public DateTime PassportIssueDate { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>(); 

        public Member Member { get; set; } = null!;
    }
}
