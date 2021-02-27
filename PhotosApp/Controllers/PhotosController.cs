using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Photos.Data;
using PhotosApp.Services;

namespace PhotosApp.Controllers
{
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly ApplicationContext _dbContext;
        private readonly ILogger<PhotosController> _logger;

        public PhotosController(ApplicationContext dbContext, ILogger<PhotosController> logger, PhotosService s)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [HttpGet("/search/{searchTerm}")]
        public IActionResult Get(string searchTerm)
        {

            return null;
        }
    }
}
