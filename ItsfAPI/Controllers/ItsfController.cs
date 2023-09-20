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
    
    [HttpGet("get-all-tournaments")]
    public IActionResult GetAllTournaments()
    {
        ResponseType type = ResponseType.Success;
        try
        {
            ICollection<TournamentDto> response = _dbHelper.GetAllTournaments();
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
    
    [HttpPost("add-tournament")]
    public IActionResult AddTournament([FromBody] TournamentDto tournamentDto)
    {
        ResponseType type = ResponseType.Success;
        try
        {
            _dbHelper.AddTournament(tournamentDto);
            return Ok(ResponseHandler.GetAppResponse(type, tournamentDto));
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
    public IActionResult UpdateGame([FromBody] ZavrsiUtakmicuDto zavrsiUtakmicuDto)
    {
        ResponseType type = ResponseType.Success;
        try
        {
            _dbHelper.UpdateGame(zavrsiUtakmicuDto.GameId,zavrsiUtakmicuDto.HostResult,zavrsiUtakmicuDto.GuestResult);
            return Ok(ResponseHandler.GetAppResponse(type, zavrsiUtakmicuDto));
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
    
    [HttpDelete("delete-tournament")]
    public IActionResult DeleteTournament([FromQuery] int tournamentId)
    {
        ResponseType type = ResponseType.Success;
        try
        {
            _dbHelper.DeleteTournament(tournamentId);
            return Ok(ResponseHandler.GetAppResponse(type, tournamentId));
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
    
    [HttpGet("get-dashboard")]
    public IActionResult GetDashboardData()
    {
        ResponseType type = ResponseType.Success;
        try
        {
            DashboardDto response = _dbHelper.GetDashboardData();
            type = ResponseType.Success;
            
            if (response is null)
            {
                type = ResponseType.NotFound;
            }

            return Ok(response);
        }
        catch (Exception e)
        {
            type = ResponseType.Failure;
            return BadRequest("Error when get dashboard data");
        }
    }
    
}