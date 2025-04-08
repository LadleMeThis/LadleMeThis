using LadleMeThis.Data.Entity;

namespace LadleMeThis.Services.FoodImageService
{
    public interface IFoodImageService
    {
        public Task<List<RecipeImage>> GetRandomFoodImagesAsync(int count);
    }
}
