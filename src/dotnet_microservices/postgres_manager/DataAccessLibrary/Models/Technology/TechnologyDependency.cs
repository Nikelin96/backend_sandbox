using Dapper;

namespace DataAccessLibrary.Models;
public sealed class TechnologyDependency
{
    public int Id { get; set; }
    public int TechnologyId { get; set; }
    public bool IsRequired { get; set; }
    public int? UnitId { get; set; }
    public int? SkillId { get; set; }
    public int? EquipmentId { get; set; }

    public List<int?> GetNotNullKeys()
    {

        return new[] { UnitId, SkillId, EquipmentId }.Where(x => x.HasValue).AsList();
    }
}
