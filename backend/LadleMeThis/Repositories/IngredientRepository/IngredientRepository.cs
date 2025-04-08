using LadleMeThis.Context;
using LadleMeThis.Data.Entity;
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

        public async Task<Ingredient> AddAsync(Ingredient ingredient)
        {
            _dbContext.Ingredients.Add(ingredient);
            await _dbContext.SaveChangesAsync();
            return ingredient;
        }

        public async Task DeleteByIdAsync(int ingredientId)
        {
            Ingredient ingredient = new() { IngredientId = ingredientId };

            _dbContext.Entry(ingredient).State = EntityState.Deleted;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Ingredient>> GetAllAsync()
        {
            return await _dbContext.Ingredients.ToListAsync();
        }

        public async Task<Ingredient> GetByIdAsync(int id)
        {
            var ingredient = await _dbContext.Ingredients.FindAsync(id);
            return ingredient ?? throw new KeyNotFoundException("No ingredient was found with given Id!");
        }

        public async Task<IEnumerable<Ingredient>> GetManyByIdAsync(int[] ingredientIds)
        {
            var ingredients = await _dbContext.Ingredients.Where(ingredient => ingredientIds.Contains(ingredient.IngredientId)).ToListAsync();
            if (ingredients.Count == 0)
            {
                throw new KeyNotFoundException("Not a single ingredient was found!");
            }
            return ingredients;
        }
    }
}
