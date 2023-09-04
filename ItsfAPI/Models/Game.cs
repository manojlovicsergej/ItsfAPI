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

    public ICollection<PlayerGames> GamePlayers { get; set; }

    public Game()
    {
        GamePlayers = new HashSet<PlayerGames>();
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