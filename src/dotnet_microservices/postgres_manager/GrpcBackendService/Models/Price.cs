namespace GrpcBackendService.Models;

public sealed class Price
{
    public int Wood { get; set; }
    public int Food { get; set; }
    public int Gold { get; set; }
    public int Stone { get; set; }
    public int? TechnologyId { get; set; }
    public int? UnitId { get; set; }
    public int? SkillId { get; set; }
    public int? EquipmentId { get; set; }
}