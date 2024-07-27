using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BlogContext _context;

        public UserRepository(BlogContext context)
        {
            _context = context;
        }

        public async Task<AppUser> GetUser()
        {
            return await _context.AppUsers.Include(u => u.ProfileImages)
                .Include(u => u.BackgroundImages)
                .FirstOrDefaultAsync();

        }
        public async Task<AppUser> GetById(int id)
        {
            return await _context.AppUsers.Include(u => u.ProfileImages).Include(u => u.BackgroundImages).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<ProfileImage>> GetProfileImages(int userId)
        {
            return await _context.ProfileImages
                .Where(i => i.AppUserId == userId && i.IsMain)
                .ToListAsync();
        }


        public async Task<IEnumerable<BackgroundImage>> GetBackgroundImages(int userId)
        {
            return await _context.BackgroundImages
                .Where(i => i.AppUserId == userId)
                .ToListAsync();
        }

        public async Task<ProfileImage> GetProfileImageById(int id)
        {
            return await _context.ProfileImages.FindAsync(id);
        }

        public async Task<BackgroundImage> GetBackgroundImageById(int id)
        {
            return await _context.BackgroundImages.FindAsync(id);
        }
        public async Task<IEnumerable<ProfileImage>> GetAllProfileImages()
        {
            return await _context.ProfileImages.ToListAsync();
        }

        public async Task<IEnumerable<BackgroundImage>> GetAllBackgroundImages()
        {
            return await _context.BackgroundImages.ToListAsync();
        }

        public async Task AddBackgroundImage(BackgroundImage backgroundImage)
        {
            await _context.BackgroundImages.AddAsync(backgroundImage);

        }

        public async Task AddProfileImage(ProfileImage profileImage)
        {
            await _context.ProfileImages.AddAsync(profileImage);

        }

        public async Task RemoveProfileImage(int id)
        {
            var profileImage = await _context.ProfileImages.FindAsync(id);
            if (profileImage != null)
            {
                _context.ProfileImages.Remove(profileImage);

            }
        }

        public async Task RemoveBackgroundImage(int id)
        {
            var backgroundImage = await _context.BackgroundImages.FindAsync(id);
            if (backgroundImage != null)
            {
                _context.BackgroundImages.Remove(backgroundImage);

            }
        }


        public void UpdateProfileImage(ProfileImage profileImage)
        {
            _context.ProfileImages.Update(profileImage);
        }


        public async Task SetMainProfileImage(int imageId)
        {
            var image = await _context.ProfileImages.FindAsync(imageId);
            if (image != null)
            {
                var currentMain = await _context.ProfileImages
                    .Where(i => i.AppUserId == image.AppUserId && i.IsMain)
                    .FirstOrDefaultAsync();
                if (currentMain != null)
                {
                    currentMain.IsMain = false;
                    _context.ProfileImages.Update(currentMain);
                }

                image.IsMain = true;
                _context.ProfileImages.Update(image);
                await _context.SaveChangesAsync();
            }
        }

        // public async Task SetMainProfileImage(int imageId)
        // {
        //     var image = await GetProfileImageById(imageId);
        //     if (image != null)
        //     {
        //         var currentMain = await _context.ProfileImages.FirstOrDefaultAsync(i => i.IsMain);
        //         if (currentMain != null)
        //         {
        //             currentMain.IsMain = false;
        //         }

        //         image.IsMain = true;
        //         await _context.SaveChangesAsync();
        //     }
        // }


    }
}