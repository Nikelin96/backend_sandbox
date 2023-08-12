namespace GrpcBackendService.Models;
public sealed class KingdomTechnology
{
    public KingdomTechnology()
    {
        //Technologies = new List<Technology>();
    }

    public string? Name { get; set; }

    public string? TechnologyName { get; set; }

    public string TechnologyDescription { get; set; }

    public DateTime ResearchStartTime { get; set; }

    public string? ResearchStatus { get; set; }

    //public List<Technology> Technologies { get; set; }
}
