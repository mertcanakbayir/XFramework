
namespace MyApp.DAL.Entities
{
    public class Page : BaseEntity
    {
        public string PageUrl { get; set; }
        public ICollection<PageRole> PageRoles { get; set; }

        public int? ParentId { get; set; }

        public Page? Parent { get; set; }

        public ICollection<Page> Children { get; set; } = new List<Page>();
    }
}
