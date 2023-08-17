namespace DataAccessLibrary.Models;

public sealed class KingdomTechnology
{
    public string? Name { get; set; }

    public string? TechnologyName { get; set; }

    public string TechnologyDescription { get; set; }

    public DateTime ResearchStartTime { get; set; }

    public string? ResearchStatus { get; set; }
}
