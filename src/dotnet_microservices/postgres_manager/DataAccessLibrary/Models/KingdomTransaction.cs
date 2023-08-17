namespace DataAccessLibrary.Models;
public class KingdomTransaction
{
    public int KingdomId { get; set; }
    public TransactionType Type { get; set; }
    public int Wood { get; set; }
    public int Food { get; set; }
    public int Gold { get; set; }
    public int Stone { get; set; }
}

public enum TransactionType
{
    Income,
    Expense
}