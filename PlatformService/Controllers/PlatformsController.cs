using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.DTOs;

namespace PlatformService.Controllers;

[Route("api/platforms")]
[ApiController]
public class PlatformsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPlatformRepo _repository;

    public PlatformsController(IPlatformRepo repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<PlatformReadDTO>> GetPlatforms()
    {
        Console.WriteLine("--> Getting Platforms... ");
        var platformItems = _repository.GetAllPlatforms();
        return Ok(_mapper.Map<IEnumerable<PlatformReadDTO>>(platformItems));
    }

    [HttpGet("{id:int}", Name = nameof(GetPlatformById))]
    public ActionResult<PlatformReadDTO> GetPlatformById(int id)
    {
        var platformItem = _repository.GetPlatformById(id);
        if (platformItem is null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<PlatformReadDTO>(platformItem));
    }
}
