using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PhotosApp.Controllers
{
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly ILogger<PhotosController> _logger;

        public PhotosController(ILogger<PhotosController> logger)
        {
            _logger = logger;
        }

        [HttpGet("/search/{searchTerm}")]
        public IActionResult Get(string searchTerm)
        {

            return null;
        }
    }
}
