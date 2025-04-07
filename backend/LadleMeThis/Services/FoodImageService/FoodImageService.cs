using System.Net.Http.Headers;
using System.Text.Json;

namespace LadleMeThis.Services.FoodImageService
{
    public class FoodImageService : IFoodImageService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly Random _random;
        public FoodImageService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _random = new Random();
            _apiKey = configuration["PEXELS_API_KEY"];
        }

        public async Task<string> GetRandomFoodImageUrlAsync()
        {
            try
            {

                int randomPage = _random.Next(101);

                var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    $"https://api.pexels.com/v1/search?query=food&page={randomPage}&per_page=1"
                );
                
                request.Headers.Add("Authorization", _apiKey);

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                
                var content = await response.Content.ReadAsStringAsync();
                
                using var doc = JsonDocument.Parse(content);
                var photos = doc.RootElement.GetProperty("photos");

                if (photos.GetArrayLength() <= 0) 
                    return "FallbackLink";
                
                var random = new Random();
                var index = random.Next(photos.GetArrayLength());
                var photo = photos[index];
                
                var src = photo.GetProperty("src");
                var imageUrl = src.GetProperty("original").GetString();

                return imageUrl ?? "ImageLink";

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return ex.Message;
            }
        }

    }
}
