using LadleMeThis.Models.CategoryModels;
using LadleMeThis.Models.IngredientsModels;
using LadleMeThis.Models.TagModels;
using LadleMeThis.Repositories.CategoryRepository;
using LadleMeThis.Repositories.IngredientRepository;
using LadleMeThis.Repositories.TagRepository;

namespace LadleMeThis.Services.RecipeDetailService
{
    /// <summary>
    /// This class is responsible to retrieve all the Ingredients, Tags and Categories.
    /// </summary>
    public class RecipeDetailService
    {
        ITagRepository _tagRepository;
        IIngredientRepository _ingredientRepository;
        ICategoryRepository _categoryRepository;

        public RecipeDetailService(ITagRepository tagRepo, IIngredientRepository ingredientRepo, ICategoryRepository categoryRepo)
        {
            _tagRepository = tagRepo;
            _categoryRepository = categoryRepo;
            _ingredientRepository = ingredientRepo;

        }


        public async Task<IEnumerable<Tag>> GetAllTags()
        {
            return await _tagRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Ingredient>> GetAllIngredients()
        {
            return await _ingredientRepository.GetAllAsync();
        }

    }
}
