using LadleMeThis.Data.Entity;
using LadleMeThis.Models.CategoryModels;
using LadleMeThis.Models.IngredientsModels;
using LadleMeThis.Models.RecipeModels;
using LadleMeThis.Models.RecipeRatingsModels;
using LadleMeThis.Models.TagModels;
using LadleMeThis.Models.UserModels;
using LadleMeThis.Repositories.RecipeRepository;
using LadleMeThis.Services.RecipeDetailService;
using LadleMeThis.Services.RecipeRatingService;
using Microsoft.AspNetCore.Identity;

namespace LadleMeThis.Services.RecipeService;

public class RecipeService(IRecipeRepository recipeRepository,
    IRecipeDetailService recipeDetailService,
    UserManager<IdentityUser> userManager) : IRecipeService
{
    public async Task<List<RecipeCardDTO>> GetAllRecipeCards()
    {
        var recipes = await recipeRepository.GetAll();

        if (recipes.Count == 0)
            return new List<RecipeCardDTO>();

        return recipes.Select(CreateRecipeCard).ToList();
    }

    public async Task<List<RecipeCardDTO>> GetRecipesByCategoryId(int categoryId)
    {
        var recipes = await recipeRepository.GetByCategoryId(categoryId);

        if (recipes.Count == 0)
            return new List<RecipeCardDTO>();

        return recipes.Select(CreateRecipeCard).ToList();
    }

    public async Task<List<RecipeCardDTO>> GetRecipesByTagId(int tagId)
    {
        var recipes = await recipeRepository.GetByTagId(tagId);

        if (recipes.Count == 0)
            return new List<RecipeCardDTO>();

        return recipes.Select(CreateRecipeCard).ToList();
    }

    public async Task<List<RecipeCardDTO>> GetRecipesByIngredientId(int ingredientId)
    {
        var recipes = await recipeRepository.GetByIngredientId(ingredientId);

        if (recipes.Count == 0)
            return new List<RecipeCardDTO>();

        return recipes.Select(CreateRecipeCard).ToList();
    }

    public async Task<List<RecipeCardDTO>> GetRecipesByIngredientIds(List<int> ingredientIds)
    {
        var recipes = await recipeRepository.GetByIngredientIds(ingredientIds);
        return recipes.Select(CreateRecipeCard).ToList();
    }

    public async Task<FullRecipeDTO> GetRecipeByRecipeId(int recipeId)
    {
        var recipe = await recipeRepository.GetByRecipeId(recipeId);
        return CreateFullRecipeDto(recipe);
    }

    public async Task<bool> DeleteRecipe(int recipeId)
    {
        return await recipeRepository.Delete(recipeId);
    }

    public async Task<int> Create(CreateRecipeDTO createRecipeDto, string userId)
    {
        var user = await GetUserById(userId);
        var recipe = CreateRecipe(createRecipeDto, user);
        return await recipeRepository.Create(recipe);
    }

    public async Task<bool> UpdateRecipe(UpdateRecipeDTO updateRecipeDto)
    {
        var recipe = await recipeRepository.GetByRecipeId(updateRecipeDto.RecipeId);

        if (updateRecipeDto.Name != null)
            recipe.Name = updateRecipeDto.Name;
        if (updateRecipeDto.Instructions != null)
            recipe.Instructions = updateRecipeDto.Instructions;
        if (updateRecipeDto.PrepTime.HasValue)
            recipe.PrepTime = updateRecipeDto.PrepTime.Value;
        if (updateRecipeDto.CookTime.HasValue)
            recipe.CookTime = updateRecipeDto.CookTime.Value;
        if (updateRecipeDto.ServingSize.HasValue)
            recipe.ServingSize = updateRecipeDto.ServingSize.Value;

        if (updateRecipeDto.Tags != null)
        {
            var newTags = await recipeDetailService.GetTagsByIds(updateRecipeDto.Tags);

            if (!recipe.Tags.SequenceEqual(newTags))
                recipe.Tags = (ICollection<Tag>)newTags;
        }

        if (updateRecipeDto.Ingredients != null)
        {
            var newIngredients = await recipeDetailService.GetIngredientsByIds(updateRecipeDto.Ingredients);

            if (!recipe.Ingredients.SequenceEqual(newIngredients))
                recipe.Ingredients = (ICollection<Ingredient>)newIngredients;
        }


        if (updateRecipeDto.Categories != null)
        {
            var newCategories = await recipeDetailService.GetCategoriesByIds(updateRecipeDto.Categories);

            if (!recipe.Categories.SequenceEqual(newCategories))
                recipe.Categories = (ICollection<Category>)newCategories;
        }

        return await recipeRepository.Update(recipe);
    }

    private RecipeCardDTO CreateRecipeCard(Recipe recipe)
    {
        var averageRating = recipe.Ratings.Any()
            ? (int)recipe.Ratings.Average(r => r.Rating)
            : 0;

        var fullTime = recipe.PrepTime + recipe.CookTime;

        var tags = recipeDetailService.GetTagDTOsByTags(recipe.Tags).ToList();
        var categories = recipeDetailService.GetCategoryDTOsByCategories(recipe.Categories).ToList();

        return new RecipeCardDTO(
            recipe.RecipeId,
            recipe.Name,
            fullTime,
            recipe.ServingSize,
            averageRating,
            tags,
            categories
        );
    }

    private Recipe CreateRecipe(CreateRecipeDTO recipeDto, IdentityUser user)
    {
        var tags = recipeDetailService.GetTagsByIds(recipeDto.Tags).Result;
        var ingredients = recipeDetailService.GetIngredientsByIds(recipeDto.Ingredients).Result;
        var categories = recipeDetailService.GetCategoriesByIds(recipeDto.Categories).Result;

        return new Recipe()
        {
            Name = recipeDto.Name,
            Instructions = recipeDto.Instructions,
            PrepTime = recipeDto.PrepTime,
            CookTime = recipeDto.CookTime,
            ServingSize = recipeDto.ServingSize,
            User = user,
            Categories = categories.ToList(),
            Tags = tags.ToList(),
            Ingredients = ingredients.ToList()
        };
    }


    private async Task<IdentityUser> GetUserById(string userId) =>
         await userManager.FindByIdAsync(userId);


    private FullRecipeDTO CreateFullRecipeDto(Recipe recipe)
    {
        var categories = recipeDetailService.GetCategoryDTOsByCategories(recipe.Categories);
        var tags = recipeDetailService.GetTagDTOsByTags(recipe.Tags);
        var ingredients = recipeDetailService.GetIngredientDTOsByIngredients(recipe.Ingredients);
        var ratings = recipeDetailService.CreateRecipeRatingDtoList(recipe.Ratings);

        return new FullRecipeDTO(
            recipe.RecipeId,
            recipe.Name,
            recipe.Instructions,
            recipe.PrepTime,
            recipe.CookTime,
            recipe.ServingSize,
            recipe.User.UserName,
            categories.ToList(),
            tags.ToList(),
            ingredients.ToList(),
            ratings
        );
    }
}