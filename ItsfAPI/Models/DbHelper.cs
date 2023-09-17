using ItsfAPI.Dto;
using ItsfAPI.EfCore;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ItsfAPI.Models;

public class DbHelper
{
    private readonly ApplicationDbContext _context;

    public DbHelper(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<PlayerDto> GetPlayers()
    {
        List<PlayerDto> players = new List<PlayerDto>();
        var list = _context.Players.ToList();

        list.ForEach(x => players.Add(new PlayerDto
        {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            DateOfBirth = x.DateOfBirth,
            Position = x.Position,
            Rating = x.Rating,
            Winrate = x.Winrate,
            Title = x.Title
        }));

        return players;
    }

    public void SavePlayers(PlayerDto playerDto)
    {
        Player player = new Player(
            playerDto.FirstName,
            playerDto.LastName,
            playerDto.DateOfBirth,
            playerDto.Position,
            playerDto.Rating,
            playerDto.Winrate,
            playerDto.Title
            );

        _context.Players.Add(player);
        _context.SaveChanges();
    }

    public void UpdatePlayer(PlayerDto playerDto)
    {
        var player = _context.Players.Where(x => x.Id == playerDto.Id).FirstOrDefault();

        if (player is not null)
        {
            player.UpdatePlayer(playerDto);
        }
        
        _context.SaveChanges();
    }

    public void DeletePlayer(int playerId)
    {
        var player = _context.Players.Where(x => x.Id == playerId).FirstOrDefault();

        if (player is not null)
        {
            _context.Players.Remove(player);
        }

        _context.SaveChanges();
    }
}