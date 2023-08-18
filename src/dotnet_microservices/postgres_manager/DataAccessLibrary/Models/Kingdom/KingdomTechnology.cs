namespace DataAccessLibrary.Models;

public sealed class KingdomTechnology
{
    public int KingdomId { get; set; }
    public int? TechnologyId { get; set; }
    public int? UnitId { get; set; }
    public int? SkillId { get; set; }
    public int? EquipmentId { get; set; }
    public int KingdomTransactionId { get; set; }
    public string? Name { get; set; }
    public string? TechnologyName { get; set; }
    public string TechnologyDescription { get; set; }
    public DateTime ResearchStartTime { get; set; }
    public ResearchStatusType ResearchStatus { get; set; }
}

public enum ResearchStatusType
{
    NotStarted,
    InProgress,
    Completed,
    Aborted
}