namespace MyApp.Dtos.SystemSettingDetail
{
    public class SystemSettingDetailDto
    {
        public int Id { get; set; }
        public int SystemSettingId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
    }
}
