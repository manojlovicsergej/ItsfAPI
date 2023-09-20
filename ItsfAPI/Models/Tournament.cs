using System.ComponentModel.DataAnnotations;

namespace ItsfAPI.Models;

public class Tournament
{
    [Key,Required]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Format { get; set; }
    public string Place { get; set; }
    public string Prize { get; set; }

    // back reference
    public ICollection<Game> Games { get; private set; }

    public Tournament()
    {
        Games = new HashSet<Game>();
    }
}