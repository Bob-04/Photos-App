using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using PhotosApp.Models;
using Newtonsoft.Json;

namespace PhotosApp.Services
{
    public class PhotosService : IPhotosService
    {
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "http://interview.agileengine.com";

        public PhotosService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient();
        }

        public async Task<string> FillPhotosAsync()
        {
            await AuthorizeClientAsync();

            var currentPage = 0;
            AgileengineImagesResponse imagesResponse = null;
            while (imagesResponse == null || imagesResponse.HasMore)
            {
                imagesResponse = await GetPicturesPage(currentPage);
            }

            return null;
        }

        private async Task AuthorizeClientAsync()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{ApiUrl}/auth")
            {
                Content = new StringContent("{\"apiKey\":\"23567b218376f79d9415\"}", Encoding.UTF8, "application/json")
            };

            var responseMessage = await _httpClient.SendAsync(requestMessage);
            responseMessage.EnsureSuccessStatusCode();

            var body = await DeserializeResponseMessage<AgileengineAuthResponse>(responseMessage);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", body.Token);
        }

        private async Task<AgileengineImagesResponse> GetPicturesPage(int page)
        {
            var responseMessage = await _httpClient.GetAsync($"{ApiUrl}/images?page={page}");
            responseMessage.EnsureSuccessStatusCode();
            return await DeserializeResponseMessage<AgileengineImagesResponse>(responseMessage);
        }

        private async Task<T> DeserializeResponseMessage<T>(HttpResponseMessage message)
        {
            var responseBody = await message.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseBody);
        }
    }
}
