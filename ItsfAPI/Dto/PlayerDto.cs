using ItsfAPI.Enums;

namespace ItsfAPI.Dto;

public class PlayerDto : IPlayerDto
{
    public int? Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Position Position { get; set; }
    public int Rating { get; set; }
    public decimal Winrate { get; set; }
    public string Title { get; set; }
}

public interface IPlayerDto
{
    int? Id { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
    DateTime DateOfBirth { get; set; }
    Position Position { get; set; }
    int Rating { get; set; }
    decimal Winrate { get; set; }
    string Title { get; set; }
}