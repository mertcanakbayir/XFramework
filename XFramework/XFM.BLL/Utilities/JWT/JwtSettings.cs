namespace XFM.BLL.Utilities.JWT
{
    public class JwtSettings
    {
        public string Key { get; set; } = "n#2UqQ$5EwFcD0z3P9lS+7z6YjWkV3FrhL!4YbS#Jh6t8^8KmT^z6J3z5sH1V85XFramework";

        public string Issuer { get; set; } = "XFramework";

        public string Audience { get; set; } = "XFramework";

        public int ExpiresInMinutes { get; set; }
    }
}
