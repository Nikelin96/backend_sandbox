namespace GrpcBackendService.Models
{
    public class Kingdom
    {
        public Kingdom()
        {
            Technologies = new List<Technology>();
        }

        public string? Name { get; set; }

        public List<Technology> Technologies { get; set; }
    }

    public class Technology
    {
        public short TechnologyId { get; set; }
        public short KingdomTransactionID { get; set; }
        public string ResearchStatus { get; set; }
    }
}
