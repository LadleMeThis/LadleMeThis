using LadleMeThis.Models.IngredientsModels;
using LadleMeThis.Models.TagModels;

namespace LadleMeThis.Repositories.IngredientRepository;

public interface IIngredientRepository
{
    public Task<IEnumerable<Ingredient>> GetAllAsync();
    public Task<Ingredient?> GetByIdAsync(int ingredientId);
    public Task<IEnumerable<Ingredient>> GetManyByIdAsync(int[] ingredientIds);
    public Task AddAsync(Ingredient ingredient);
    public Task DeleteByIdAsync(int ingredientId);
}