﻿using AutoMapper;
using TravelAgencyBackend.Models;
using TravelAgencyBackend.ViewModels;

namespace TravelAgencyBackend.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Role, RoleViewModel>().ReverseMap();
            CreateMap<Permission, PermissionViewModel>().ReverseMap();
        }
    }
}
