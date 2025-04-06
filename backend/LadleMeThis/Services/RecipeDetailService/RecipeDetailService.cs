using LadleMeThis.Data.Entity;
using LadleMeThis.Models.CategoryModels;
using LadleMeThis.Models.IngredientsModels;
using LadleMeThis.Models.RecipeRatingsModels;
using LadleMeThis.Models.TagModels;
using LadleMeThis.Repositories.CategoryRepository;
using LadleMeThis.Repositories.IngredientRepository;
using LadleMeThis.Repositories.TagRepository;
using LadleMeThis.Services.RecipeRatingService;
using Microsoft.AspNetCore.Identity;

namespace LadleMeThis.Services.RecipeDetailService
{
    /// <summary>
    /// This class is responsible to retrieve all the Ingredients, Tags and Categories.
    /// </summary>
    public class RecipeDetailService(
        ITagRepository tagRepo,
        IIngredientRepository ingredientRepo,
        ICategoryRepository categoryRepo,
        IRecipeRatingService recipeRatingService)
        : IRecipeDetailService
    {
        IRecipeRatingService _recipeRatingService = recipeRatingService;

        public async Task<IEnumerable<Tag>> GetTagsByIds(int[] tagIds)
        {
            return await tagRepo.GetManyByIdAsync(tagIds);
        }

        public async Task<IEnumerable<Category>> GetCategoriesByIds(int[] categoryIds)
        {
            return await categoryRepo.GetManyByIdAsync(categoryIds);
        }

        public async Task<IEnumerable<Ingredient>> GetIngredientsByIds(int[] ingredientIds)
        {
            return await ingredientRepo.GetManyByIdAsync(ingredientIds);
        }


        public IEnumerable<TagDTO> GetTagDTOsByTags(IEnumerable<Tag> tags)
        {
            return tags.Select(tag => new TagDTO() { Name = tag.Name, TagId = tag.TagId });
        }
        public IEnumerable<IngredientDTO> GetIngredientDTOsByIngredients(IEnumerable<Ingredient> ingredients)
        {
            return ingredients.Select(ingredient => new IngredientDTO() { Name = ingredient.Name, IngredientId = ingredient.IngredientId, Unit = ingredient.Unit });
        }
        public IEnumerable<CategoryDTO> GetCategoryDTOsByCategories(IEnumerable<Category> categories)
        {
            return categories.Select(category => new CategoryDTO() { Name = category.Name, CategoryId = category.CategoryId });
        }

        public  List<RecipeRatingDTO> CreateRecipeRatingDtoList(IEnumerable<RecipeRating> ratings) => 
           recipeRatingService.CreateRecipeRatingDtoList(ratings).Result; 
        
        public async Task<int> AddRecipeRatingAsync(CreateRecipeRatingDTO recipeRatingDto, IdentityUser user, Recipe recipe) => 
            await _recipeRatingService.CreateRecipeRating(recipeRatingDto, user, recipe);
        
    }
}
