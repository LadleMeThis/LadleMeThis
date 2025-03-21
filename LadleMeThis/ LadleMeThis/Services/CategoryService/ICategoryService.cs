using LadleMeThis.Models.CategoryModels;

namespace LadleMeThis.Services.CategoryService
{
    public interface ICategoryService
    {
        public Task<IEnumerable<CategoryDTO>> GetAllAsync();
        public Task<CategoryDTO?> GetByIdAsync(int categoryId);
        public Task<IEnumerable<CategoryDTO>> GetManyByIdAsync(int[] categoryIds);
        public Task<CategoryDTO> AddAsync(CategoryCreateRequest categoryCreateRequest);
        public Task DeleteByIdAsync(int categoryId);
    }
}
