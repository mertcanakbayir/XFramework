namespace MyApp.Dtos.Endpoint
{
    public class EndpointDto
    {
        public int Id { get; set; }
        public string Controller { get; init; }

        public string Action { get; init; }

        public string HttpMethod { get; init; }
    }
}
