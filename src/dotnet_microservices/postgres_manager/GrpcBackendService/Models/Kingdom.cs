namespace GrpcBackendService.Models
{
    public sealed class Kingdom
    {
        public Kingdom()
        {
            Technologies = new List<Technology>();
        }

        public string? Name { get; set; }

        public List<Technology> Technologies { get; set; }
    }
}
