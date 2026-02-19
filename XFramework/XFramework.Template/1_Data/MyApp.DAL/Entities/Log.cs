namespace MyApp.DAL.Entities
{
    public class Log
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string? MessageTemplate { get; set; }
        public string Level { get; set; }
        public DateTime TimeStamp { get; set; }
        public string? Exception { get; set; }
        public string? Properties { get; set; }
        public string? UserId { get; set; }
        public string? IPAddress { get; set; }
        public string? ActionName { get; set; }
        public string? TraceIdentifier { get; set; }
    }
}
