using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ItsfAPI.Enums;

namespace ItsfAPI.Models;

[Table("playergames")]
public class PlayerGames
{
    [Key,Required]
    public int Id { get; set; }
    
    public int PlayerId { get; set; }
    public Player Player { get; set; }

    public int GameId { get; set; }
    public Game Game { get; set; }

    public Side Side { get; set; }
    
    public PlayerGames(){}

    public PlayerGames(int playerId, int gameId, Side side)
    {
        PlayerId = playerId;
        GameId = gameId;
        Side = side;
    }
}