namespace API.DTOs
{
    public class ArticleDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public string Headline { get; set; }
        public bool IsEvent { get; set; }
        public string YouTubeLink { get; set; }
        public string FacebookLink { get; set; }
        public List<ImageDto> Images { get; set; }
        public List<VideoDto> Videos { get; set; }

    }
}