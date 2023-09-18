using ItsfAPI.Enums;

namespace ItsfAPI.Dto;

public class PlayerGamesDto : IPlayerGamesDto
{
    public int? Id { get; set; }
    public int PlayerId { get; set; }
    public int? GameId { get; set; }
    public Side Side { get; set; }
}

public interface IPlayerGamesDto
{
    public int? Id { get; set; }
    int PlayerId { get; set; }
    int? GameId { get; set; }
    public Side Side { get; set; }
}