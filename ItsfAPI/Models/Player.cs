using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ItsfAPI.Enums;

namespace ItsfAPI.Models;

[Table("player")]
public class Player
{
    [Key,Required]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Position Position { get; set; }
    public int Rating { get; set; }
    public decimal Winrate { get; set; }
    public string Title { get; set; }

    // FKs
    public ICollection<PlayerGames> PlayerGames { get; set; }

    public Player()
    {
        PlayerGames = new HashSet<PlayerGames>();
    }

    public Player(string firstName, string lastName, DateTime dateOfBirth, Position position, int rating,
        decimal winrate, string title)
    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Position = position;
        Rating = rating;
        Winrate = winrate;
        Title = title;
    }
}