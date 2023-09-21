using ItsfAPI.Dto;
using ItsfAPI.EfCore;
using Microsoft.EntityFrameworkCore;

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
        var list = _context.Games
            .Include(x => x.GamePlayers)
            .Where(x => x.HostResult !=0 || x.GuestResult != 0)
            .ToList();

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
    
    public void DeleteTournament(int tournamentId)
    {
        var tournament = _context.Tournaments.Where(x => x.Id == tournamentId).FirstOrDefault();
        var games = _context.Games.Where(x => x.TournamentId == tournament.Id).ToList();

        foreach (var game in games)
        { 
            game.UpdateGameToTournament(null);
            _context.Update(game);
            _context.SaveChanges();
        }

        if (tournament is not null)
        {
            _context.Tournaments.Remove(tournament);
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
    
    public List<TournamentDto> GetAllTournaments()
    {
        List<TournamentDto> tournaments = new List<TournamentDto>();
        var list = _context.Tournaments.ToList();

        list.ForEach(x => tournaments.Add(new TournamentDto()
        {
            Id = x.Id,
            Name = x.Name,
            Format = x.Format,
            Place = x.Place,
            Prize = x.Prize
        }));

        return tournaments;
    }
    
    public void AddTournament(TournamentDto tournamentDto)
    {
        Tournament tournament = new Tournament()
        {
            Name = tournamentDto.Name,
            Format = tournamentDto.Format,
            Place = tournamentDto.Place,
            Prize = tournamentDto.Prize,
        };
        
        _context.Tournaments.Add(tournament);
        _context.SaveChanges();

        foreach (GameDto gameDto in tournamentDto.Games)
        {
            Game game = _context.Games.Where(x => x.Id == gameDto.Id).FirstOrDefault();
            game.UpdateGameToTournament(tournament.Id);
            
            _context.Games.Update(game);
            _context.SaveChanges();
        }
    }
    
    public DashboardDto GetDashboardData()
    {
        int brojIgraca = _context.Players.Count();
        int brojUtakmica = _context.Games.Count();
        int brojTurnira = _context.Tournaments.Count();

        Player igrac = _context.Players.OrderByDescending(x => x.Id).FirstOrDefault();
        Tournament turnir = _context.Tournaments.OrderByDescending(x => x.Id).FirstOrDefault();
        Game utakmica = _context.Games.OrderByDescending(x => x.Id).FirstOrDefault();

        PlayerDto igracDto = new PlayerDto()
        {
            FirstName = igrac.FirstName,
            LastName = igrac.LastName,
            Position = igrac.Position,
            Rating = igrac.Rating,
            Winrate = igrac.Winrate,
            Title = igrac.Title
        };

        TournamentDto turnirDto = new TournamentDto()
        {
            Name = turnir.Name,
            Format = turnir.Format,
            Place = turnir.Place,
            Prize = turnir.Prize
        };

        GameDto utakmicaDto = new GameDto()
        {
            GameName = utakmica.GameName,
            HostName = utakmica.HostName,
            HostResult = utakmica.HostResult,
            GuestName = utakmica.GuestName,
            GuestResult = utakmica.GuestResult
        };

        return new DashboardDto()
        {
            BrojIgraca = brojIgraca,
            BrojUtakmica = brojUtakmica,
            BrojTurnira = brojTurnira,
            Igrac = igracDto,
            Turnir = turnirDto,
            Utakmica = utakmicaDto
        };
    }
}