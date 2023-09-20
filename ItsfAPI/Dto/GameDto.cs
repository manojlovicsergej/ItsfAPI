namespace ItsfAPI.Dto;

public class GameDto : IGameDto
{
    public int? Id { get; set; }
    public string GameName { get; set; }
    public string HostName { get; set; }
    public int HostResult { get; set; }
    public string GuestName { get; set; }
    public int GuestResult { get; set; }
    public ICollection<PlayerGamesDto> GamePlayers { get; set; }
}

public interface IGameDto
{
    int? Id { get; set; }
    string GameName { get; set; }

    string HostName { get; set; }
    int HostResult { get; set; }

    string GuestName { get; set; }
    int GuestResult { get; set; }

    ICollection<PlayerGamesDto> GamePlayers { get; set; }
}