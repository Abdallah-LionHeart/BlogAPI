namespace API.DTOs
{
    public class ArticleCreateDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Headline { get; set; }
        public bool IsEvent { get; set; }
        public string YouTubeLink { get; set; }
        public string FacebookLink { get; set; }
        public List<ImageDto> Images { get; set; } = new List<ImageDto>();
        public List<VideoDto> Videos { get; set; } = new List<VideoDto>();
    }
}
