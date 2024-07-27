using API.DTOs;
using API.Entities;
using AutoMapper;
using CloudinaryDotNet;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, AppUserDto>()
               .ForMember(dest => dest.ProfileImageUrl, opt => opt.MapFrom(src => src.ProfileImages.FirstOrDefault(pi => pi.IsMain).Url))
               .ForMember(dest => dest.BackgroundImageUrl, opt => opt.MapFrom(src => src.BackgroundImages.FirstOrDefault().Url));
            CreateMap<ProfileImage, ProfileImageDto>();
            CreateMap<BackgroundImage, BackgroundImageDto>();
            CreateMap<AppUserDto, AppUser>();
            CreateMap<ProfileImageDto, ProfileImage>();
            CreateMap<BackgroundImageDto, BackgroundImage>();

            CreateMap<ArticleDto, Article>().ReverseMap();
            CreateMap<ArticleCreateDto, Article>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
                .ForMember(dest => dest.Videos, opt => opt.MapFrom(src => src.Videos));
            CreateMap<Article, ArticleCreateDto>()
                    .ForMember(dest => dest.Images, opt => opt.Ignore())
                    .ForMember(dest => dest.Videos, opt => opt.Ignore())
                    .ReverseMap();

            CreateMap<Article, ArticleUpdateDto>()
                .ForMember(dest => dest.Images, opt => opt.Ignore())
                .ForMember(dest => dest.Videos, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Image, ImageDto>()
                .ReverseMap();

            CreateMap<Video, VideoDto>()
                .ReverseMap();
            // CreateMap<Article, ArticleDto>().ReverseMap();
            // CreateMap<Article, ArticleCreateDto>().ReverseMap();
            // CreateMap<Article, ArticleUpdateDto>().ReverseMap();
            // CreateMap<Image, ImageDto>().ReverseMap();
            // CreateMap<Video, VideoDto>().ReverseMap();
        }

    }
}