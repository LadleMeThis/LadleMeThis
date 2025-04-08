using LadleMeThis.Data.Entity;
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

        public async Task<List<RecipeImage>> GetRandomFoodImagesAsync(int count)
        {
            var results = new List<RecipeImage>();
            var seenImageIds = new HashSet<int>();
            int curentPage = 0;

            while (results.Count < count)
            {
                int remaining = count - results.Count;
                int fetchCount = Math.Min(80, remaining);

                var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    $"https://api.pexels.com/v1/search?query=food&page={curentPage}&per_page={fetchCount}"
                );

                request.Headers.Add("Authorization", _apiKey);

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(content);

                if (!doc.RootElement.TryGetProperty("photos", out var photos) || photos.GetArrayLength() == 0)
                {
                    continue;
                }

                foreach (var photo in photos.EnumerateArray())
                {
                    var id = photo.GetProperty("id").GetInt32();
                    if (seenImageIds.Contains(id))
                    {
                        continue;
                    }

                    seenImageIds.Add(id);

                    var src = photo.GetProperty("src");
                    var imageUrl = src.GetProperty("original").GetString();
                    var photographer = photo.GetProperty("photographer").GetString();
                    var photographerUrl = photo.GetProperty("photographer_url").GetString();
                    var alt = photo.GetProperty("alt").GetString();

                    results.Add(new RecipeImage()
                    {
                        ImageUrl = imageUrl,
                        PhotographerName = photographer,
                        PhotographerUrl = photographerUrl,
                        Alt = alt
                    });

                    if (results.Count >= count)
                    {
                        break;
                    }
                    curentPage++;
                }
            }

            return results;
        }

    }
}
