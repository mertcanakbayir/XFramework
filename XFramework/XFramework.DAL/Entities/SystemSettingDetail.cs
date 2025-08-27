using XFramework.Helper.Enums;

namespace XFramework.DAL.Entities
{
    public class SystemSettingDetail : BaseEntity
    {
        public int SystemSettingId { get; set; }
        public SystemSetting SystemSetting { get; set; }
        public string Key { get; set; }

        public string Value { get; set; }

        public SystemSettingType Type { get; set; }
    }
}
