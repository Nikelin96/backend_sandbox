namespace DataAccessLibrary.Models;
public sealed class Skill
{
    public SkillType Type { get; set; }

    public int StatId { get; set; }
}


public enum SkillType
{
    //[PgName("attack")]
    Attack,
    //[PgName("defend")]
    Defend,
    //[PgName("heal")]
    Heal,
    //[PgName("kick")]
    Kick,
    //[PgName("bash")]
    Bash,
    //[PgName("dash")]
    Dash,
    //[PgName("rally")]
    Rally
}