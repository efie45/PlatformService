using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.DTOs;
using PlatformService.Models;
using PlatformService.SyncDataService.Http;

namespace PlatformService.Controllers;

[Route("api/platforms")]
[ApiController]
public class PlatformsController : ControllerBase
{
    private readonly ICommandDataClient _commandDataClient;
    private readonly IMapper _mapper;
    private readonly IPlatformRepo _repository;

    public PlatformsController(IPlatformRepo repository, IMapper mapper, ICommandDataClient commandDataClient)
    {
        _commandDataClient = commandDataClient;
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

    [HttpPost]
    public async Task<ActionResult<PlatformReadDTO>> CreatePlatform(PlatformCreateDTO platformCreateDTO)
    {
        var platformModel = _mapper.Map<Platform>(platformCreateDTO);
        _repository.CreatePlatform(platformModel);
        _repository.SaveChanges();
        var platformReadDto = _mapper.Map<PlatformReadDTO>(platformModel);

        try
        {
            await _commandDataClient.SendPlatformToCommand(platformReadDto);
        }
        catch (Exception exception)
        {
            Console.WriteLine($"--> Could not send sync data to command service: {exception.Message}");
        }

        return CreatedAtRoute(nameof(GetPlatformById), new {platformReadDto.Id}, platformReadDto);
    }
}

