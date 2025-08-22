namespace XFramework.DAL.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsActive { get; set; } = true;
        public int CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }
    }
}
