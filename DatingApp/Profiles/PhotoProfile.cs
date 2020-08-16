using AutoMapper;
using DatingApp.Dtos.Photo;
using DatingApp.Models;

namespace DatingApp.Profiles
{
    public class PhotoProfile : Profile
    {
        public PhotoProfile()
        {
            CreateMap<Photo, PhotoForDetailsDto>();
            CreateMap<PhotoForCreationDto, Photo>();
        }
    }
}