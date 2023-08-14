namespace GrpcBackendService.Models;
public sealed class UnitDto
{
    public string Name { get; set; }

    public int StatId { get; set; }

    public int KingdomId { get; set; }

}

public sealed class Stat
{
    public int HealthPoints { get; set; }
    public int DefensePoints { get; set; }
}

