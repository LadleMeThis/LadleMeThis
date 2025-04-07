using LadleMeThis.Data.Entity;
using LadleMeThis.Models.RecipeModels;
using LadleMeThis.Models.RecipeRatingsModels;
using LadleMeThis.Models.UserModels;
using LadleMeThis.Repositories.RecipeRepository;

namespace LadleMeThis.Services.RecipeService;

public interface IRecipeService
{
    Task<List<RecipeCardDTO>> GetAllRecipeCards();
    Task<List<RecipeCardDTO>> GetRecipesByName(string name);
    Task<List<RecipeCardDTO>> GetRecipesByCategoryId(int categoryId);
    Task<List<RecipeCardDTO>> GetRecipesByCategroryIdAndName(int categroryId, string recipeName);
    Task<List<RecipeCardDTO>> GetRecipesByTagId(int tagId);
    Task<List<RecipeCardDTO>> GetRecipesByIngredientId(int ingredientId);
    Task<List<RecipeCardDTO>> GetRecipesByIngredientIds(List<int> ingredientIds);
    Task<FullRecipeDTO> GetRecipeByRecipeId(int recipeId);
    Task<bool> DeleteRecipe(int recipeId);
    Task<int> Create(CreateRecipeDTO createRecipeDto, string userId);
    Task<bool> UpdateRecipe(UpdateRecipeDTO updateRecipeDto);
    Task<List<RecipeCardDTO>> GetRecipesByUserId(string userId);
    Task<int> CreateRecipeRatingById(int recipeId, string userId, CreateRecipeRatingDTO recipeRatingDto);
}