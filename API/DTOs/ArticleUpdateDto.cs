namespace API.DTOs
{
    public class ArticleUpdateDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Headline { get; set; }
        public bool IsEvent { get; set; }
        public string YouTubeLink { get; set; }
        public string FacebookLink { get; set; }
        public List<IFormFile> Images { get; set; }
        public List<IFormFile> Videos { get; set; }
    }
}
