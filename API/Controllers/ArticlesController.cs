using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly ArticleService _service;
        private readonly IMapper _mapper;

        public ArticlesController(ArticleService service, IMapper mapper)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleDto>>> GetArticles()
        {
            return Ok(await _service.GetAllArticles());
        }

        [HttpGet("events")]
        public async Task<ActionResult<IEnumerable<ArticleDto>>> GetEvents()
        {
            return Ok(await _service.GetAllEvents());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleDto>> GetArticle(int id)
        {
            var article = await _service.GetArticleById(id);
            if (article == null)
            {
                return NotFound();
            }
            return Ok(article);
        }

        [HttpPost("add-article")]
        public async Task<ActionResult<ArticleDto>> CreateArticle([FromForm] ArticleCreateDto articleDto, [FromForm] List<IFormFile> images, [FromForm] List<IFormFile> videos)
        {
            var createdArticle = await _service.AddArticle(articleDto, images, videos);
            return CreatedAtAction(nameof(GetArticle), new { id = createdArticle.Id }, createdArticle);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArticle(int id, [FromForm] ArticleUpdateDto articleDto, [FromForm] List<IFormFile> images, [FromForm] List<IFormFile> videos)
        {
            if (id != articleDto.Id)
            {
                return BadRequest();
            }

            await _service.UpdateArticle(articleDto, images, videos);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            await _service.DeleteArticle(id);
            return NoContent();
        }

        [HttpPost("{id}/images")]
        public async Task<ActionResult> AddImage(int id, [FromForm] IFormFile file)
        {
            await _service.AddImage(file, id);
            return Ok();
        }

        [HttpDelete("images/{id}")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            await _service.RemoveImage(id);
            return NoContent();
        }
        [HttpPost("{id}/videos")]
        public async Task<ActionResult> AddVideo(int id, [FromForm] IFormFile file)
        {
            await _service.AddVideo(file, id);
            return Ok();
        }


        [HttpDelete("videos/{id}")]
        public async Task<IActionResult> DeleteVideo(int id)
        {
            await _service.RemoveVideo(id);
            return NoContent();
        }

        [HttpGet("videos/{id}")]
        public async Task<ActionResult<Video>> GetVideo(int id)
        {
            var video = await _service.GetVideoById(id);
            if (video == null)
            {
                return NotFound();
            }
            return Ok(video);
        }

        [HttpGet("images/{id}")]
        public async Task<ActionResult<Image>> GetImage(int id)
        {
            var image = await _service.GetImageById(id);
            if (image == null)
            {
                return NotFound();
            }
            return Ok(image);
        }

        [HttpGet("paginated")]
        public async Task<ActionResult<PaginatedResult<ArticleDto>>> GetPaginatedArticles([FromQuery] ArticleParams articleParams)
        {
            var result = await _service.GetPaginatedArticles(articleParams);
            return Ok(result);
        }


        [HttpGet("search")]
        public async Task<ActionResult<PaginatedResult<ArticleDto>>> SearchArticles([FromQuery] ArticleParams articleParams, [FromQuery] string searchTerm, [FromQuery] string filter)
        {
            var result = await _service.SearchArticles(articleParams, searchTerm, filter);
            return Ok(result);
        }

    }
}
