namespace MyApp.DAL.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public bool IsActive { get; set; } = true;
        public int? DeletedBy { get; set; }
        public DateTime DeletedAt { get; set; }
        public int Revision { get; set; } = 0;

    }
}
