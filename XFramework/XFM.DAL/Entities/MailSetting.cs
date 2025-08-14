using XFM.DAL.Entities;

namespace XFramework.DAL.Entities
{
    public class MailSetting:BaseEntity
    {
        public string SmtpHost { get; set; }

        public int SmtpPort { get; set; }

        public string SmtpUser { get; set; }

        public string EncryptedPassword { get; set; }

        public bool EnableSsl { get; set; }

        public string SenderEmail { get; set; }
    }
}
