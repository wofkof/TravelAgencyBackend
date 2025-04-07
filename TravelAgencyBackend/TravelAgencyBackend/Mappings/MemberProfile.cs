using AutoMapper;
using TravelAgencyBackend.Models;
using TravelAgencyBackend.ViewModels;

namespace TravelAgencyBackend.Mappings
{
    public class MemberProfile : Profile
    {
        public MemberProfile()
        {
            CreateMap<Member, MemberCreateViewModel>().ReverseMap();
            CreateMap<Member, MemberEditViewModel>().ReverseMap();
            CreateMap<Member, MemberDetailViewModel>();
            CreateMap<Member, MemberListItemViewModel>();
        }
    }
}
