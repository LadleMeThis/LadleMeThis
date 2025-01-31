using LadleMeThis.Context;
using LadleMeThis.Models.CategoryModels;
using LadleMeThis.Models.TagModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace LadleMeThis.Repositories.CategoryRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        LadleMeThisContext _dbContext;
        public CategoryRepository(LadleMeThisContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Category category)
        {
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();
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

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _dbContext.Categories.FindAsync(id);
        }

        public async Task<IEnumerable<Category>> GetManyByIdAsync(int[] categoryIds)
        {
            return await _dbContext.Categories.Where(category => categoryIds.Contains(category.CategoryId)).ToListAsync();
        }
    }
}
