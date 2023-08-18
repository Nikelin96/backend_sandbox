namespace DataAccessLibrary.Models;
public sealed class KingdomUnit
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int KingdomId { get; set; }
    public int UnitId { get; set; }
    public int KingdomTransactionId { get; set; }
}
