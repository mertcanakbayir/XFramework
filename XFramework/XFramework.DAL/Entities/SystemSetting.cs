
namespace XFramework.DAL.Entities
{
    public class SystemSetting : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<SystemSettingDetail> SystemSettingDetails { get; set; }
    }
}
