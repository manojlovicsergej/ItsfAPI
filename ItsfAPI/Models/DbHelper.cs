using ItsfAPI.Dto;
using ItsfAPI.EfCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing.Template;

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

    public List<GameDto> GetAllGames()
    {
        List<GameDto> games = new List<GameDto>();
        var list = _context.Games.ToList();

        list.ForEach(x => games.Add(new GameDto
        {
            Id = x.Id,
            GameName = x.GameName,
            HostName = x.HostName,
            HostResult = x.HostResult,
            GuestName = x.GuestName,
            GuestResult = x.GuestResult
        }));

        return games;
    }

    public List<GameDto> GetCurrentGames()
    {
        List<GameDto> games = new List<GameDto>();
        var list = _context.Games.Where(x => x.HostResult == 0 && x.GuestResult == 0).ToList();

        list.ForEach(x => games.Add(new GameDto
        {
            Id = x.Id,
            GameName = x.GameName,
            HostName = x.HostName,
            HostResult = x.HostResult,
            GuestName = x.GuestName,
            GuestResult = x.GuestResult
        }));

        return games;
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

    public void UpdateGame(int gameId, int hostResult, int guestResult)
    {
        var game = _context.Games.Where(x => x.Id == gameId).FirstOrDefault();

        if (game is not null)
        {
            game.UpdateGameResult(hostResult, guestResult);
        }

        _context.Games.Update(game);
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

    public void DeleteGame(int gameId)
    {
        var game = _context.Games.Where(x => x.Id == gameId).FirstOrDefault();

        if (game is not null)
        {
            _context.Games.Remove(game);
        }

        _context.SaveChanges();
    }

    public void AddGame(GameDto gameDto)
    {
        Game game = new Game
        {
            GameName = gameDto.GameName,
            HostName = gameDto.HostName,
            HostResult = gameDto.HostResult,
            GuestName = gameDto.GuestName,
            GuestResult = gameDto.GuestResult
        };

        _context.Games.Add(game);
        _context.SaveChanges();

        foreach (var player in gameDto.GamePlayers)
        {
            var gamePlayer = new PlayerGames
            {
                GameId = game.Id,
                PlayerId = player.PlayerId,
                Side = player.Side
            };

            _context.PlayerGames.Add(gamePlayer);
        }

        _context.SaveChanges();
    }
}