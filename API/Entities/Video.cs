namespace API.Entities
{
    public class Video
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsExternal { get; set; }
        public string PublicId { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; }

    }
}