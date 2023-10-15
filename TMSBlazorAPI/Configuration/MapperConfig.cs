using AutoMapper;
using TMSBlazorAPI.Data;
using TMSBlazorAPI.Models.Club;

namespace TMSBlazorAPI.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<ClubCreateDto,Club > ().ReverseMap();
            CreateMap<ClubReadOnlyDto,Club > ().ReverseMap();
            CreateMap<ClubUpdateDto,Club > ().ReverseMap();
        }
    }
}
