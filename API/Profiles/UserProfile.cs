using API.Models;
using AutoMapper;
using Domain.Entities;

namespace API.Profiles
{

    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserCreateDto, User>()
                .ForMember(desc => desc.CreatedTime, cgf => cgf.MapFrom(src => DateTime.Today))
                .ForMember(desc => desc.UserStateId, cfg => cfg.MapFrom(src => 1));
        }
    }
}
