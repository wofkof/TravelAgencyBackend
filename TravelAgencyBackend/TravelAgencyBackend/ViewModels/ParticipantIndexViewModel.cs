using TravelAgencyBackend.Models;

namespace TravelAgencyBackend.ViewModels
{
    public class ParticipantIndexViewModel
    {
        public string? SearchText { get; set; }
        public int? FilterMemberId { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalCount { get; set; }

        public List<Participant> Participants { get; set; } = new List<Participant>();
        public List<Member> Members { get; set; } = new List<Member>();
    }
}
