using ItsfAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ItsfAPI.EfCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options){}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.UseSerialColumns(); // this will automatically defines my incremental operations
    }
    
    public DbSet<Player> Players { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<PlayerGames> PlayerGames { get; set; }
    public DbSet<Tournament> Tournaments { get; set; }

}