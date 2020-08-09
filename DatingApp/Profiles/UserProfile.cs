using AutoMapper;
using DatingApp.Dtos.User;
using DatingApp.Helpers;
using DatingApp.Models;
using System.Linq;

namespace DatingApp.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserForListDto>()
            .ForMember(
                dest => dest.PhotoUrl,
                opts => opts.MapFrom(src => src.Photos.FirstOrDefault(u => u.IsMain).Url)
            )
            .ForMember(
                dest => dest.Age,
                opts => opts.MapFrom(src => src.DateOfBirth.TillNow())
            );

            CreateMap<User, UserForDetailsDto>()
            .ForMember(
                dest => dest.PhotoUrl,
                opts => opts.MapFrom(src => src.Photos.FirstOrDefault(u => u.IsMain).Url)
            )
            .ForMember(
                dest => dest.Age,
                opts => opts.MapFrom(src => src.DateOfBirth.TillNow())
            );


            CreateMap<UserForEdit, User>();
        }
    }
}