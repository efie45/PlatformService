using AutoMapper;
using PlatformService.DTOs;
using PlatformService.Models;

namespace PlatformService.AutomapperProfiles;

public class PlatformProfiles : Profile
{
    public PlatformProfiles()
    {
        // Source -> Target
        CreateMap<Platform, PlatformReadDTO>();
        CreateMap<PlatformCreateDTO, Platform>();
    }
}
