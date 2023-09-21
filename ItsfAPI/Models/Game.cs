using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItsfAPI.Models;

[Table("game")]
public class Game
{
    [Key,Required]
    public int Id { get; set; }
    public string GameName { get; set; }

    public string HostName { get; set; }
    public int HostResult { get; set; }

    public string GuestName { get; set; }
    public int GuestResult { get; set; }
    
    public int? TournamentId { get; set; }
    public Tournament Tournament { get; set; }

    public ICollection<PlayerGames> GamePlayers { get; set; }

    public Game()
    {
        GamePlayers = new HashSet<PlayerGames>();
    }
    
    public void UpdateGameResult(int hostResult, int guestResult)
    {
        HostResult = hostResult;
        GuestResult = guestResult;
    }

    public void UpdateGameToTournament(int? tournamentId)
    {
        TournamentId = tournamentId;
    }
    
    public Game(string gameName, string hostName, int hostResult, string guestName, int guestResult)
    {
        GuestName = guestName;
        GuestResult = guestResult;
        HostName = hostName;
        HostResult = hostResult;
        GameName = gameName;
    }
}