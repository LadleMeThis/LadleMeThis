using LadleMeThis.Context;
using LadleMeThis.Models.IngredientsModels;
using LadleMeThis.Models.TagModels;
using Microsoft.EntityFrameworkCore;

namespace LadleMeThis.Repositories.IngredientRepository
{
    public class IngredientRepository : IIngredientRepository
    {

        private LadleMeThisContext _dbContext;

        public IngredientRepository(LadleMeThisContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Ingredient ingredient)
        {
            _dbContext.Ingredients.Add(ingredient);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int ingredientId)
        {
            Ingredient ingredient = new() { Id = ingredientId };

            _dbContext.Entry(ingredient).State = EntityState.Deleted;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Ingredient>> GetAllAsync()
        {
            return await _dbContext.Ingredients.ToListAsync();
        }

        public async Task<Ingredient?> GetByIdAsync(int id)
        {
            return await _dbContext.Ingredients.FindAsync(id);
        }

        public async Task<IEnumerable<Ingredient>> GetManyByIdAsync(int[] ingredientIds)
        {
            return await _dbContext.Ingredients.Where(ingredient => ingredientIds.Contains(ingredient.Id)).ToListAsync();
        }
    }
}
