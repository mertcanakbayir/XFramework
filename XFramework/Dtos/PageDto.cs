namespace XFramework.Dtos
{
    public class PageDto
    {
        public string PageUrl { get; init; }
        public int ParentId { get; set; }

        public List<PageDto> Children { get; set; }
    }
}
