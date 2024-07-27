using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;

namespace API.Services
{
    public class ArticleService
    {
        private readonly IUnitOfWork _uow;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IMapper _mapper;

        public ArticleService(IUnitOfWork unitOfWork, ICloudinaryService cloudinaryService, IMapper mapper)
        {
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
            _uow = unitOfWork;

        }

        public async Task<IEnumerable<ArticleDto>> GetAllArticles()
        {
            var articles = await _uow.Articles.GetAll();
            return _mapper.Map<IEnumerable<ArticleDto>>(articles);
        }

        public async Task<IEnumerable<ArticleDto>> GetAllEvents()
        {
            var events = await _uow.Articles.GetAllEvents();
            return _mapper.Map<IEnumerable<ArticleDto>>(events);
        }

        public async Task<ArticleDto> GetArticleById(int id)
        {
            var article = await _uow.Articles.GetById(id);
            return _mapper.Map<ArticleDto>(article);
        }
        public async Task<ArticleDto> AddArticle(ArticleCreateDto articleDto, List<IFormFile> imageFiles, List<IFormFile> videoFiles)
        {
            var article = new Article
            {
                Title = articleDto.Title,
                Content = articleDto.Content,
                Headline = articleDto.Headline,
                IsEvent = articleDto.IsEvent,
                YouTubeLink = articleDto.YouTubeLink,
                FacebookLink = articleDto.FacebookLink
            };

            if (imageFiles != null && imageFiles.Any())
            {
                article.Images = new List<Image>();
                foreach (var imageFile in imageFiles)
                {
                    var uploadResult = await _cloudinaryService.UploadImageAsync(imageFile);
                    var image = new Image
                    {
                        Url = uploadResult.SecureUrl.AbsoluteUri,
                        PublicId = uploadResult.PublicId
                    };
                    article.Images.Add(image);
                }
            }

            if (videoFiles != null && videoFiles.Any())
            {
                article.Videos = new List<Video>();
                foreach (var videoFile in videoFiles)
                {
                    var uploadResult = await _cloudinaryService.UploadVideoAsync(videoFile);
                    var video = new Video
                    {
                        Url = uploadResult.SecureUrl.AbsoluteUri,
                        PublicId = uploadResult.PublicId,
                        IsExternal = false
                    };
                    article.Videos.Add(video);
                }
            }

            if (!string.IsNullOrEmpty(articleDto.YouTubeLink))
            {
                var video = new Video
                {
                    Url = articleDto.YouTubeLink,
                    IsExternal = true
                };
                article.Videos.Add(video);
            }

            await _uow.Articles.Add(article);
            await _uow.CompleteAsync();

            return _mapper.Map<ArticleDto>(article);
        }

        public async Task UpdateArticle(ArticleUpdateDto articleDto, List<IFormFile> imageFiles, List<IFormFile> videoFiles)
        {
            var article = await _uow.Articles.GetById(articleDto.Id);
            if (article == null)
            {
                throw new Exception("Article not found");
            }

            article.Title = articleDto.Title;
            article.Content = articleDto.Content;
            article.Headline = articleDto.Headline;
            article.IsEvent = articleDto.IsEvent;
            article.YouTubeLink = articleDto.YouTubeLink;
            article.FacebookLink = articleDto.FacebookLink;

            if (imageFiles != null && imageFiles.Any())
            {
                foreach (var imageFile in imageFiles)
                {
                    var uploadResult = await _cloudinaryService.UploadImageAsync(imageFile);
                    var image = new Image
                    {
                        Url = uploadResult.SecureUrl.AbsoluteUri,
                        PublicId = uploadResult.PublicId
                    };
                    article.Images.Add(image);
                }
            }

            if (videoFiles != null && videoFiles.Any())
            {
                foreach (var videoFile in videoFiles)
                {
                    var uploadResult = await _cloudinaryService.UploadVideoAsync(videoFile);
                    var video = new Video
                    {
                        Url = uploadResult.SecureUrl.AbsoluteUri,
                        PublicId = uploadResult.PublicId,
                        IsExternal = false
                    };
                    article.Videos.Add(video);
                }
            }

            if (!string.IsNullOrEmpty(articleDto.YouTubeLink))
            {
                var video = new Video
                {
                    Url = articleDto.YouTubeLink,
                    IsExternal = true
                };
                article.Videos.Add(video);
            }

            await _uow.Articles.Update(article);
            await _uow.CompleteAsync();
        }


        // public async Task AddAedrticle(ArticleCreateDto articleDto)
        // {
        //     var article = _mapper.Map<Article>(articleDto);
        //     if (articleDto.Images != null)
        //     {
        //         foreach (var imageFile in articleDto.Images)
        //         {
        //             var uploadResult = await _cloudinaryService.UploadImageAsync(imageFile);
        //             var image = new Image
        //             {
        //                 Url = uploadResult.SecureUrl.AbsoluteUri,
        //                 PublicId = uploadResult.PublicId
        //             };
        //             article.Images.Add(image);
        //         }
        //     }

        //     if (articleDto.Videos != null)
        //     {
        //         foreach (var videoFile in articleDto.Videos)
        //         {
        //             var uploadResult = await _cloudinaryService.UploadVideoAsync(videoFile);
        //             var video = new Video
        //             {
        //                 Url = uploadResult.SecureUrl.AbsoluteUri,
        //                 PublicId = uploadResult.PublicId,
        //                 IsExternal = false
        //             };
        //             article.Videos.Add(video);
        //         }
        //     }

        //     if (!string.IsNullOrEmpty(articleDto.YouTubeLink))
        //     {
        //         var video = new Video
        //         {
        //             Url = articleDto.YouTubeLink,
        //             IsExternal = true
        //         };
        //         article.Videos.Add(video);
        //     }

        //     await _uow.Articles.Add(article);
        //     await _uow.CompleteAsync();
        // }

        // public async Task UpdateArticle(ArticleUpdateDto articleDto)
        // {
        //     var article = await _uow.Articles.GetById(articleDto.Id);
        //     if (article == null)
        //     {
        //         throw new Exception("Article not found");
        //     }

        //     _mapper.Map(articleDto, article);

        //     if (articleDto.Images != null)
        //     {
        //         foreach (var imageFile in articleDto.Images)
        //         {
        //             var uploadResult = await _cloudinaryService.UploadImageAsync(imageFile);
        //             var image = new Image
        //             {
        //                 Url = uploadResult.SecureUrl.AbsoluteUri,
        //                 PublicId = uploadResult.PublicId
        //             };
        //             article.Images.Add(image);
        //         }
        //     }

        //     if (articleDto.Videos != null)
        //     {
        //         foreach (var videoFile in articleDto.Videos)
        //         {
        //             var uploadResult = await _cloudinaryService.UploadVideoAsync(videoFile);
        //             var video = new Video
        //             {
        //                 Url = uploadResult.SecureUrl.AbsoluteUri,
        //                 PublicId = uploadResult.PublicId,
        //                 IsExternal = false
        //             };
        //             article.Videos.Add(video);
        //         }
        //     }

        //     if (!string.IsNullOrEmpty(articleDto.YouTubeLink))
        //     {
        //         var video = new Video
        //         {
        //             Url = articleDto.YouTubeLink,
        //             IsExternal = true
        //         };
        //         article.Videos.Add(video);
        //     }

        //     await _uow.Articles.Update(article);
        //     await _uow.CompleteAsync();
        // }

        public async Task DeleteArticle(int id)
        {
            await _uow.Articles.Delete(id);
            await _uow.CompleteAsync();
        }


        public async Task AddImage(IFormFile file, int articleId)
        {
            var uploadResult = await _cloudinaryService.UploadImageAsync(file);
            var image = new Image
            {
                Url = uploadResult.SecureUrl.AbsoluteUri,
                PublicId = uploadResult.PublicId,
                ArticleId = articleId
            };

            await _uow.Articles.AddImage(image);
            await _uow.CompleteAsync();
        }
        public async Task RemoveImage(int id)
        {
            var image = await _uow.Articles.GetImageById(id);
            if (image != null)
            {
                await _cloudinaryService.DeleteFileAsync(image.PublicId);
                await _uow.Articles.RemoveImage(id);
                await _uow.CompleteAsync();
            }
        }

        public Task<Image> GetImageById(int id)
        {
            return _uow.Articles.GetImageById(id);
        }

        public async Task AddVideo(IFormFile file, int articleId)
        {
            var uploadResult = await _cloudinaryService.UploadVideoAsync(file);
            var video = new Video
            {
                Url = uploadResult.SecureUrl.AbsoluteUri,
                PublicId = uploadResult.PublicId,
                ArticleId = articleId,
                IsExternal = false
            };

            await _uow.Articles.AddVideo(video);
            await _uow.CompleteAsync();
        }

        public async Task RemoveVideo(int id)
        {
            var video = await _uow.Articles.GetVideoById(id);
            if (video != null && !video.IsExternal)
            {
                await _cloudinaryService.DeleteFileAsync(video.PublicId);
                await _uow.Articles.RemoveVideo(id);
                await _uow.CompleteAsync();
            }
        }

        public Task<Video> GetVideoById(int id)
        {
            return _uow.Articles.GetVideoById(id);
        }

        public async Task<PaginatedResult<ArticleDto>> GetPaginatedArticles(ArticleParams articleParams)
        {
            var result = await _uow.Articles.GetPaginated(articleParams);
            var articlesDto = _mapper.Map<IEnumerable<ArticleDto>>(result.Items);
            return new PaginatedResult<ArticleDto>(articlesDto, result.TotalCount, result.PageNumber, result.PageSize);
        }

        public async Task<PaginatedResult<ArticleDto>> SearchArticles(ArticleParams articleParams, string searchTerm, string filter)
        {
            var result = await _uow.Articles.SearchArticles(articleParams, searchTerm, filter);
            var articlesDto = _mapper.Map<IEnumerable<ArticleDto>>(result.Items);
            return new PaginatedResult<ArticleDto>(articlesDto, result.TotalCount, result.PageNumber, result.PageSize);
        }

    }
}
