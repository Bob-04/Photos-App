using AutoMapper;
using Photos.Data.Models;
using PhotosApp.Models;

namespace PhotosApp.Mapping
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AgileengineImageResponse, Image>();
        }
    }
}
