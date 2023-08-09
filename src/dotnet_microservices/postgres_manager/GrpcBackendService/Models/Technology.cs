using System.ComponentModel.DataAnnotations.Schema;

namespace GrpcBackendService.Models
{
    public sealed class Technology
    {
        public short Id { get; set; }

        public short KingdomId { get; set; }
        public short KingdomTransactionID { get; set; }

        public string ResearchStatus { get; set; }

        public DateTime ResearchStartTime { get; set; }
    }
}
