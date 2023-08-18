namespace DataAccessLibrary.Models;
public sealed class UnitTechnology
{
    public int TechnologyId { get; set; }

    public bool IsRequired { get; set; }

    public string TechnologyName { get; set; }

    public int UnitId { get; set; }

    public int? SkillId { get; set; }

    public int? EquipmentId { get; set; }
}
