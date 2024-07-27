namespace API.Entities
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; } = DateTime.UtcNow;
        public string Headline { get; set; }
        public bool IsEvent { get; set; }
        public string YouTubeLink { get; set; }
        public string FacebookLink { get; set; }
        public List<Image> Images { get; set; } = new List<Image>();
        public List<Video> Videos { get; set; } = new List<Video>();
    }
}