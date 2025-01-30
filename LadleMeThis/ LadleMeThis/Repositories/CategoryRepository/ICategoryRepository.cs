using LadleMeThis.Models.CategoryModels;
using LadleMeThis.Models.IngredientsModels;

namespace LadleMeThis.Repositories.CategoryRepository;

public interface ICategoryRepository
{
    public Task<IEnumerable<Category>> GetAllAsync();
    public Task<Category?> GetByIdAsync(int categoryId);
    public Task<IEnumerable<Category>> GetManyByIdAsync(int[] categoryIds);
    public Task AddAsync(Category category);
    public Task DeleteByIdAsync(int categoryId);
}