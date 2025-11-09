namespace MyApp.Dtos
{
    public class MailSettingDto
    {
        public string SmtpHost { get; set; } = string.Empty;
        public int SmtpPort { get; set; } = 25;
        public string SmtpUser { get; set; } = string.Empty;
        public string EncryptedPassword { get; set; } = string.Empty;
        public bool EnableSsl { get; set; } = false;
        public string SenderEmail { get; set; } = string.Empty;
        public bool IsSmtp { get; set; } = true;
    }
}
