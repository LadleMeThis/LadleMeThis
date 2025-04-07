namespace LadleMeThis.Services.FoodImageService
{
    public interface IFoodImageService
    {
        public Task<string> GetRandomFoodImageUrlAsync();

    }
}
