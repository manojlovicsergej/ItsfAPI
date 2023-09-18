using ItsfAPI.Dto;
using ItsfAPI.EfCore;
using ItsfAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ItsfAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItsfController : ControllerBase
{
    private readonly DbHelper _dbHelper;
    
    public ItsfController(ApplicationDbContext context)
    {
        _dbHelper = new DbHelper(context);
    }

    [HttpGet]
    [Route("get-all-players")]
    public IActionResult Get()
    {
        ResponseType type = ResponseType.Success;
        try
        {
            ICollection<PlayerDto> response = _dbHelper.GetPlayers();
            type = ResponseType.Success;
            if (!response.Any())
            {
                type = ResponseType.NotFound;
            }

            return Ok(response);
        }
        catch (Exception e)
        {
            type = ResponseType.Failure;
            return BadRequest("Error when get all players");
        }
    }
    
    [HttpGet("get-all-games")]
    public IActionResult GetAllGames()
    {
        ResponseType type = ResponseType.Success;
        try
        {
            ICollection<GameDto> response = _dbHelper.GetAllGames();
            type = ResponseType.Success;
            if (!response.Any())
            {
                type = ResponseType.NotFound;
            }

            return Ok(response);
        }
        catch (Exception e)
        {
            type = ResponseType.Failure;
            return BadRequest("Error when get all games");
        }
    }
    
    [HttpGet("get-current-games")]
    public IActionResult GetCurrentGames()
    {
        ResponseType type = ResponseType.Success;
        try
        {
            ICollection<GameDto> response = _dbHelper.GetCurrentGames();
            type = ResponseType.Success;
            if (!response.Any())
            {
                type = ResponseType.NotFound;
            }

            return Ok(response);
        }
        catch (Exception e)
        {
            type = ResponseType.Failure;
            return BadRequest("Error when get all games");
        }
    }

    [HttpPost("add-player")]
    public IActionResult AddPlayer([FromBody] PlayerDto playerDto)
    {
        ResponseType type = ResponseType.Success;
        try
        {
            _dbHelper.SavePlayers(playerDto);
            return Ok(ResponseHandler.GetAppResponse(type, playerDto));
        }
        catch (Exception e)
        {
            return BadRequest("Error when adding player");
        }
    }
    
    [HttpPost("add-game")]
    public IActionResult AddGame([FromBody] GameDto gameDto)
    {
        ResponseType type = ResponseType.Success;
        try
        {
            _dbHelper.AddGame(gameDto);
            return Ok(ResponseHandler.GetAppResponse(type, gameDto));
        }
        catch (Exception e)
        {
            return BadRequest("Error when adding game");
        }
    }

    [HttpPut("update-player")]
    public IActionResult UpdatePlayer([FromBody] PlayerDto playerDto)
    {
        ResponseType type = ResponseType.Success;
        try
        {
            _dbHelper.UpdatePlayer(playerDto);
            return Ok(ResponseHandler.GetAppResponse(type, playerDto));
        }
        catch (Exception e)
        {
            return BadRequest("Error when updating player");
        }
    }
    
    [HttpPut("update-game")]
    public IActionResult UpdateGame([FromQuery] int gameId, [FromQuery] int hostResult, [FromQuery] int guestResult)
    {
        ResponseType type = ResponseType.Success;
        try
        {
            _dbHelper.UpdateGame(gameId,hostResult,guestResult);
            return Ok(ResponseHandler.GetAppResponse(type, gameId));
        }
        catch (Exception e)
        {
            return BadRequest("Error when updating game");
        }
    }

    [HttpDelete("delete-player")]
    public IActionResult DeletePlayer([FromQuery] int playerId)
    {
        ResponseType type = ResponseType.Success;
        try
        {
            _dbHelper.DeletePlayer(playerId);
            return Ok(ResponseHandler.GetAppResponse(type, playerId));
        }
        catch (Exception e)
        {
            return BadRequest("Error when adding player");
        }
    }
    
    [HttpDelete("delete-game")]
    public IActionResult DeleteGame([FromQuery] int gameId)
    {
        ResponseType type = ResponseType.Success;
        try
        {
            _dbHelper.DeleteGame(gameId);
            return Ok(ResponseHandler.GetAppResponse(type, gameId));
        }
        catch (Exception e)
        {
            return BadRequest("Error when adding player");
        }
    }
    
}