using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleBackend.Server.Services.Contracts;

namespace SampleBackend.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet("{userIdentifier}")]
        public async Task<IActionResult> GetImageUrl(string userIdentifier)
        {
            var imageUrl = await _imageService.GetImageUrl(userIdentifier);
            return Ok(new { imageUrl });
        }
    }
}
