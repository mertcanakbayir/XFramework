namespace MyApp.Dtos.Endpoint
{
    public class EndpointAddDto
    {
        public string Controller { get; set; }

        public string Action { get; set; }

        public string HttpMethod { get; set; }
    }
}
