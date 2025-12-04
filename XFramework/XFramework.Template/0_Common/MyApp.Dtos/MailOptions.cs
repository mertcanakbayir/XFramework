namespace MyApp.Dtos
{
    public class MailOptions
    {
        public string SmtpHost { get; set; } = string.Empty;
        public int SmtpPort { get; set; } = 25;
        public string SmtpUser { get; set; } = string.Empty;
        public string SmtpPassword { get; set; } = string.Empty;
        public string SenderEmail { get; set; } = string.Empty;
        public bool IsQueue { get; set; }
    }
}
