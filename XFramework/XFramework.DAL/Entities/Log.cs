namespace XFramework.DAL.Entities
{
    public class Log
    {
        public int Id { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public string? ActionName { get; set; }
        public string? Exception { get; set; }
        public int? UserId { get; set; }
        public string? IpAddress { get; set; }
    }
}
