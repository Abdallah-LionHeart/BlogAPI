using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("BackgroundImages")]
    public class BackgroundImage
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
