using AutoMapper;
using TravelAgencyBackend.Models;
using TravelAgencyBackend.ViewModels;

namespace TravelAgencyBackend.Mappings
{
    public class ParticipantProfile : Profile
    {
        public ParticipantProfile()
        {

            CreateMap<Participant, ParticipantEditViewModel>().ReverseMap();
            CreateMap<Participant, ParticipantCreateViewModel>().ReverseMap();

            // 詳細資料：Entity ➜ DetailViewModel
            CreateMap<Participant, ParticipantDetailViewModel>()
                .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.Member.Name));

            // 列表用：Entity ➜ ListItemViewModel
            CreateMap<Participant, ParticipantListItemViewModel>()
                .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.Member.Name))
                .ForMember(dest => dest.MemberAccount, opt => opt.MapFrom(src => src.Member.Account));

        }
    }
}
