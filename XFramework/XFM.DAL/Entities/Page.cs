using XFM.DAL.Entities;

namespace XFramework.DAL.Entities
{
    public class Page:BaseEntity
    {
        public int Id { get; set; }

        public string PageUrl { get; set; }
        public ICollection<PageRole> PageRoles { get; set; }
    }
}
