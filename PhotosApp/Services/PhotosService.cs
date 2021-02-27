using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PhotosApp.Models;
using Newtonsoft.Json;
using Photos.Data;
using Photos.Data.Models;

namespace PhotosApp.Services
{
    public class PhotosService : IPhotosService
    {
        private readonly ApplicationContext _dbContext;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private const string ApiUrl = "http://interview.agileengine.com";

        public PhotosService(ApplicationContext dbContext, IHttpClientFactory clientFactory,
            IConfiguration configuration, IMapper mapper)
        {
            _dbContext = dbContext;
            _httpClient = clientFactory.CreateClient();
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task FillPhotosAsync()
        {
            await AuthorizeClientAsync();

            var currentPage = 0;
            AgileengineImagesResponse imagesResponse = null;
            while (imagesResponse == null || imagesResponse.HasMore)
            {
                imagesResponse = await GetImagePage(currentPage);

                var notExistsImageIds = await GetNotExistsImageIds(imagesResponse);

                foreach (var imageId in notExistsImageIds)
                {
                    var imageResponse = await GetImage(imageId);
                    var image = _mapper.Map<Image>(imageResponse);
                    _dbContext.Images.Add(image);
                }

                await _dbContext.SaveChangesAsync();

                currentPage++;
            }
        }

        private async Task AuthorizeClientAsync()
        {
            var authParams = new AgileengineAuthParams
            {
                ApiKey = _configuration["Agileengine:ApiKey"]
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{ApiUrl}/auth")
            {
                Content = new StringContent(JsonConvert.SerializeObject(authParams), Encoding.UTF8, "application/json")
            };

            var responseMessage = await _httpClient.SendAsync(requestMessage);
            responseMessage.EnsureSuccessStatusCode();

            var body = await DeserializeResponseMessage<AgileengineAuthResponse>(responseMessage);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", body.Token);
        }

        private async Task<AgileengineImagesResponse> GetImagePage(int page)
        {
            var responseMessage = await _httpClient.GetAsync($"{ApiUrl}/images?page={page}");
            responseMessage.EnsureSuccessStatusCode();
            return await DeserializeResponseMessage<AgileengineImagesResponse>(responseMessage);
        }

        private async Task<AgileengineImageResponse> GetImage(string id)
        {
            var responseMessage = await _httpClient.GetAsync($"{ApiUrl}/images/{id}");
            responseMessage.EnsureSuccessStatusCode();
            return await DeserializeResponseMessage<AgileengineImageResponse>(responseMessage);
        }

        private async Task<IEnumerable<string>> GetNotExistsImageIds(AgileengineImagesResponse response)
        {
            var pictureIds = response.Pictures.Select(p => p.Id);

            var containsIds = await _dbContext.Images
                .Where(i => pictureIds.Contains(i.Id))
                .Select(i => i.Id)
                .ToListAsync();

            return pictureIds.Where(id => !containsIds.Contains(id));
        }

        private async Task<T> DeserializeResponseMessage<T>(HttpResponseMessage message)
        {
            var responseBody = await message.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseBody);
        }
    }
}
