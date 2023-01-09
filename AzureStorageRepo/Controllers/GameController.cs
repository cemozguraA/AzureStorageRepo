using AzureStorageRepo.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AzureStorageRepo.Controllers;
[Route("api/[controller]")]
[ApiController]
public class GameController : ControllerBase
{
    private readonly IGameRepository _gameRepository;

    public GameController(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {

        return Ok(await _gameRepository.GetAllAsync());
    }

}
