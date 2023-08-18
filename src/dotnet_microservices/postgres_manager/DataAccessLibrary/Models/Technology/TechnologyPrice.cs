
namespace DataAccessLibrary.Models;
public sealed class TechnologyPrice
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? Wood { get; set; }
    public int? Food { get; set; }
    public int? Gold { get; set; }
    public int? Stone { get; set; }
}
