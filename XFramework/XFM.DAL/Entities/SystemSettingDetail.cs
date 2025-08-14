using XFM.DAL.Entities;

namespace XFramework.DAL.Entities
{
    public class SystemSettingDetail:BaseEntity
    {
        public int SystemSettingId { get; set; }
        public SystemSetting SystemSetting { get; set; }
        public int Key { get; set; }

        public string Value { get; set; }

        public string Type { get; set; }
    }
}
