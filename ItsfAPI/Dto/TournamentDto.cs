using ItsfAPI.Models;

namespace ItsfAPI.Dto;

public class TournamentDto : ITournamentDto
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public string Format { get; set; }
    public string Place { get; set; }
    public string Prize { get; set; }
    public List<GameDto>? Games { get; set; }
}

public interface ITournamentDto
{
    int? Id { get; set; }
    string Name { get; set; }
    string Format { get; set; }
    string Place { get; set; }
    string Prize { get; set; }
    List<GameDto>? Games { get; set; }
}