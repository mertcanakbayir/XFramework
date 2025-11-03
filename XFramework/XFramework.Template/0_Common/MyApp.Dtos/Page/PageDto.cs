namespace MyApp.Dtos.Page
{
    public class PageDto
    {
        public int Id { get; set; }
        public string PageUrl { get; set; }
        public int ParentId { get; set; }
        public List<PageDto> Children { get; set; }
    }
}
