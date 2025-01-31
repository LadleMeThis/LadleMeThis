using LadleMeThis.Models.CategoryModels;
using LadleMeThis.Models.IngredientsModels;
using LadleMeThis.Repositories.CategoryRepository;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LadleMeThis.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        ICategoryRepository _categoryRepository;


        public CategoryService(ICategoryRepository categoryRepo)
        {
            _categoryRepository = categoryRepo;
        }

        public async Task<CategoryDTO> AddAsync(CategoryCreateRequest categoryCreateRequest)
        {
            var category = await _categoryRepository.AddAsync(new Category() { Name = categoryCreateRequest.Name });
            return new CategoryDTO() { Name = category.Name, CategoryId = category.CategoryId };
        }
        public async Task DeleteByIdAsync(int categoryId)
        {
            await _categoryRepository.DeleteByIdAsync(categoryId);
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories.Select(category => new CategoryDTO() { Name = category.Name, }).ToList();
        }

        public async Task<CategoryDTO> GetByIdAsync(int categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            return new CategoryDTO() { Name = category.Name };
        }

        public async Task<IEnumerable<CategoryDTO>> GetManyByIdAsync(int[] categoryIds)
        {
            var categories = await _categoryRepository.GetManyByIdAsync(categoryIds);
            return categories.Select(category => new CategoryDTO() { Name = category.Name, }).ToList();
        }
    }
}