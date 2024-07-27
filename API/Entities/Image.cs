namespace API.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; }

    }
}