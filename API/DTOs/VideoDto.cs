namespace API.DTOs
{
    public class VideoDto
    {

        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsExternal { get; set; }
        public string PublicId { get; set; }
    }
}