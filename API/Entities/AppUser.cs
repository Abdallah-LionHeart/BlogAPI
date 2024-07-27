using System.Text.Json.Serialization;

namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Education { get; set; }
        public string Experience { get; set; }
        public string Position { get; set; }
        public string Overview { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }
        public string FacebookLink { get; set; }
        public string TwitterLink { get; set; }
        public string YouTubeLink { get; set; }
        [JsonIgnore]
        public List<ProfileImage> ProfileImages { get; set; }
        [JsonIgnore]
        public List<BackgroundImage> BackgroundImages { get; set; }
    }
}