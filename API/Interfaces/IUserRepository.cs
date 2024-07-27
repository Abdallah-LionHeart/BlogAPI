using API.Entities;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        Task<AppUser> GetUser();
        Task<AppUser> GetById(int id);
        Task Update(AppUser user);
        Task<IEnumerable<ProfileImage>> GetProfileImages(int userId);
        Task<IEnumerable<BackgroundImage>> GetBackgroundImages(int userId);
        Task AddBackgroundImage(BackgroundImage backgroundImage);
        Task AddProfileImage(ProfileImage profileImage);
        Task RemoveProfileImage(int id);
        Task RemoveBackgroundImage(int id);
        Task<ProfileImage> GetProfileImageById(int id);
        Task<BackgroundImage> GetBackgroundImageById(int id);
        Task SetMainProfileImage(int imageId);

        Task<IEnumerable<ProfileImage>> GetAllProfileImages();
        Task<IEnumerable<BackgroundImage>> GetAllBackgroundImages();

    }
}