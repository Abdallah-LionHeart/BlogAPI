using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;

namespace API.Services
{
    public class UserService
    {
        private readonly IUnitOfWork _uow;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, ICloudinaryService cloudinaryService, IMapper mapper)
        {
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
            _uow = unitOfWork;
        }
        public async Task<AppUserDto> GetUser()
        {
            var user = await _uow.Users.GetUser();
            return _mapper.Map<AppUserDto>(user);
        }
        public async Task<AppUserDto> GetUserById(int id)
        {
            var user = await _uow.Users.GetById(id);
            return _mapper.Map<AppUserDto>(user);
        }

        public async Task UpdateUser(AppUserDto userDto)
        {
            var user = _mapper.Map<AppUser>(userDto);
            await _uow.Users.Update(user);
            await _uow.CompleteAsync();
        }
        public async Task<IEnumerable<ProfileImageDto>> GetUserProfileImages(int userId)
        {
            var images = await _uow.Users.GetProfileImages(userId);
            return _mapper.Map<IEnumerable<ProfileImageDto>>(images);
        }

        public async Task<IEnumerable<BackgroundImageDto>> GetUserBackgroundImages(int userId)
        {
            var images = await _uow.Users.GetProfileImages(userId);
            return _mapper.Map<IEnumerable<BackgroundImageDto>>(images);
        }

        public async Task<ProfileImageDto> AddProfileImage(IFormFile file, int userId)
        {
            var uploadResult = await _cloudinaryService.UploadImageAsync(file);
            var profileImage = new ProfileImage
            {
                Url = uploadResult.SecureUrl.AbsoluteUri,
                PublicId = uploadResult.PublicId,
                AppUserId = userId
            };

            await _uow.Users.AddProfileImage(profileImage);
            await _uow.CompleteAsync();

            return _mapper.Map<ProfileImageDto>(profileImage);
        }

        public async Task<BackgroundImageDto> AddBackgroundImage(IFormFile file, int userId)
        {
            var uploadResult = await _cloudinaryService.UploadImageAsync(file);
            var backgroundImage = new BackgroundImage
            {
                Url = uploadResult.SecureUrl.AbsoluteUri,
                PublicId = uploadResult.PublicId,
                AppUserId = userId
            };

            await _uow.Users.AddBackgroundImage(backgroundImage);
            await _uow.CompleteAsync();

            return _mapper.Map<BackgroundImageDto>(backgroundImage);
        }

        public async Task<ProfileImageDto> RemoveUserProfileImage(int id)
        {
            var image = await _uow.Users.GetProfileImageById(id);
            if (image != null)
            {
                await _cloudinaryService.DeleteFileAsync(image.PublicId);
                await _uow.Users.RemoveProfileImage(id);
                await _uow.CompleteAsync();
            }

            return _mapper.Map<ProfileImageDto>(image);
        }

        public async Task<BackgroundImageDto> RemoveUserBackgroundImage(int id)
        {
            var image = await _uow.Users.GetBackgroundImageById(id);
            if (image != null)
            {
                await _cloudinaryService.DeleteFileAsync(image.PublicId);
                await _uow.Users.RemoveBackgroundImage(id);
                await _uow.CompleteAsync();
            }

            return _mapper.Map<BackgroundImageDto>(image);
        }

        public Task<ProfileImage> GetProfileImageById(int id)
        {
            return _uow.Users.GetProfileImageById(id);
        }

        public Task<BackgroundImage> GetBackgroundImageById(int id)
        {
            return _uow.Users.GetBackgroundImageById(id);
        }



        public async Task<IEnumerable<ProfileImageDto>> GetAllProfileImages()
        {
            var images = await _uow.Users.GetAllProfileImages();
            return _mapper.Map<IEnumerable<ProfileImageDto>>(images);
        }

        public async Task<IEnumerable<BackgroundImageDto>> GetAllBackgroundImages()
        {
            var images = await _uow.Users.GetAllBackgroundImages();
            return _mapper.Map<IEnumerable<BackgroundImageDto>>(images);
        }


        public async Task<ProfileImageDto> SetMainProfileImage(int imageId)
        {
            await _uow.Users.SetMainProfileImage(imageId);
            await _uow.CompleteAsync();

            var image = await _uow.Users.GetProfileImageById(imageId);
            return _mapper.Map<ProfileImageDto>(image);
        }


        // public async Task SetMainProfileImage(int imageId)
        // {
        //     await _uow.Users.SetMainProfileImage(imageId);
        //     await _uow.CompleteAsync();
        // }
    }
}
