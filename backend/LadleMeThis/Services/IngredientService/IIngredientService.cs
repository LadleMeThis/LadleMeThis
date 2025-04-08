using LadleMeThis.Models.IngredientsModels;

namespace LadleMeThis.Services.IngredientService
{
    public interface IIngredientService
    {
        public Task<IEnumerable<IngredientDTO>> GetAllAsync();
        public Task<IngredientDTO> GetByIdAsync(int ingredientId);
        public Task<IEnumerable<IngredientDTO>> GetManyByIdAsync(int[] ingredientIds);
        public Task<IngredientDTO> AddAsync(IngredientCreateRequest ingredientCreateRequest);
        public Task DeleteByIdAsync(int ingredientId);
    }
}