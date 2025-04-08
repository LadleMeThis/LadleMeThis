using LadleMeThis.Context;
using LadleMeThis.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace LadleMeThis.Repositories.CategoryRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        LadleMeThisContext _dbContext;
        public CategoryRepository(LadleMeThisContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Category> AddAsync(Category category)
        {
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();
            return category;
        }

        public async Task DeleteByIdAsync(int categoryId)
        {
            Category category = new() { CategoryId = categoryId };

            _dbContext.Entry(category).State = EntityState.Deleted;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            var category = await _dbContext.Categories.FindAsync(id);
            return category ?? throw new KeyNotFoundException("No category was found with given Id!");
        }

        public async Task<IEnumerable<Category>> GetManyByIdAsync(int[] categoryIds)
        {
            var categories = await _dbContext.Categories.Where(category => categoryIds.Contains(category.CategoryId)).ToListAsync();
            if (categories.Count == 0)
            {
                throw new KeyNotFoundException("Not a single category was found!");
            }
            return categories;
        }
    }
}
