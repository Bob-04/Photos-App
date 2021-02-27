using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Photos.Data;
using Photos.Data.Models;

namespace PhotosApp.Controllers
{
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly ApplicationContext _dbContext;
        private readonly ILogger<PhotosController> _logger;

        public PhotosController(ApplicationContext dbContext, ILogger<PhotosController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [HttpGet("/search/{searchTerm}")]
        public async Task<IEnumerable<Image>> SearchImages(string searchTerm)
        {
            _logger.LogInformation($"Getting images by term ${searchTerm}");

            var images = await _dbContext.Images
                .Where(i =>
                    i.Camera.ToLower().Contains(searchTerm.ToLower()) ||
                    i.Author.ToLower().Contains(searchTerm.ToLower()) ||
                    i.Tags.ToLower().Contains(searchTerm.ToLower()))
                .ToListAsync();

            return images;
        }
    }
}
