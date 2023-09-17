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
    
}