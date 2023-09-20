namespace ItsfAPI.Dto;

public class DashboardDto
{
    public int BrojIgraca { get; set; }
    public int BrojUtakmica { get; set; }
    public int BrojTurnira { get; set; }
    
    public PlayerDto Igrac { get; set; }
    public GameDto Utakmica { get; set; }
    public TournamentDto Turnir { get; set; }
}