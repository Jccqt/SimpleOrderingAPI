namespace ApiGateway.Models
{
    public class ApiRoute
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string DestinationUrl { get; set; }
    }
}
