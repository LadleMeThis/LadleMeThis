using LadleMeThis.Context;
using LadleMeThis.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace LadleMeThis.Repositories.RecipeRepository;

public class RecipeRepository(LadleMeThisContext ladleMeThisContext) : IRecipeRepository
{
    public async Task<List<Recipe>> GetAll() =>
        await ladleMeThisContext.Recipes
            .Include(r => r.Categories)
            .Include(r => r.Tags)
            .Include(r => r.Ingredients)
            .Include(r => r.Ratings)
            .Include(r => r.User)
            .Include(r => r.RecipeImage)
            .ToListAsync();

    public async Task<List<Recipe>> GetByCategoryId(int categoryId) =>
        await ladleMeThisContext.Recipes
            .Include(r => r.Categories)
            .Include(r => r.Tags)
            .Include(r => r.Ingredients)
            .Include(r => r.Ratings)
            .Include(r => r.User)
            .Include(r => r.RecipeImage)
            .Where(r => r.Categories.Any(c => c.CategoryId == categoryId))
            .ToListAsync();

    public async Task<List<Recipe>> GetByTagId(int tagId) =>
        await ladleMeThisContext.Recipes
            .Include(r => r.Categories)
            .Include(r => r.Tags)
            .Include(r => r.Ingredients)
            .Include(r => r.Ratings)
            .Include(r => r.User)
            .Include(r => r.RecipeImage)
            .Where(r => r.Tags.Any(t => t.TagId == tagId))
            .ToListAsync();

    public async Task<List<Recipe>> GetByIngredientId(int ingredientId) =>
        await ladleMeThisContext.Recipes
            .Include(r => r.Categories)
            .Include(r => r.Tags)
            .Include(r => r.Ingredients)
            .Include(r => r.Ratings)
            .Include(r => r.User)
            .Include(r => r.RecipeImage)
            .Where(r => r.Ingredients.Any(i => i.IngredientId == ingredientId))
            .ToListAsync();

    public async Task<List<Recipe>> GetByIngredientIds(List<int> ingredientIds) =>
        await ladleMeThisContext.Recipes
            .Include(r => r.Categories)
            .Include(r => r.Tags)
            .Include(r => r.Ingredients)
            .Include(r => r.Ratings)
            .Include(r => r.User)
            .Include(r => r.RecipeImage)
            .Where(r => r.Ingredients.Any(i => ingredientIds.Contains(i.IngredientId)))
            .ToListAsync();

    public async Task<List<Recipe>> GetByUserId(string userId) =>
        await ladleMeThisContext.Recipes
            .Include(r => r.Categories)
            .Include(r => r.Tags)
            .Include(r => r.Ingredients)
            .Include(r => r.Ratings)
            .Include(r => r.User)
            .Include(r => r.RecipeImage)
            .Where(r => r.User.Id == userId)
            .ToListAsync();

    public async Task<Recipe> GetByRecipeId(int recipeId)
    {
        var recipe = await ladleMeThisContext.Recipes
            .Include(r => r.Categories)
            .Include(r => r.Tags)
            .Include(r => r.Ingredients)
            .Include(r => r.Ratings)
            .Include(r => r.User)
            .Include(r => r.RecipeImage)
            .FirstOrDefaultAsync(r => r.RecipeId == recipeId);

        return recipe ?? throw new KeyNotFoundException("Recipe not found");
    }

    public async Task<int> Create(Recipe recipe)
    {
        if (recipe == null) throw new ArgumentNullException(nameof(recipe));

        try
        {
            ladleMeThisContext.Recipes.Add(recipe);
            await ladleMeThisContext.SaveChangesAsync();
            return recipe.RecipeId;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating recipe: {ex.Message}");
            throw new InvalidOperationException("An error occurred while creating the recipe");
        }
    }

    public async Task<bool> Update(Recipe recipe)
    {
        if (recipe == null) throw new ArgumentNullException(nameof(recipe));

        try
        {
            var existingRecipe = await GetByRecipeId(recipe.RecipeId);
            if (existingRecipe == null) throw new KeyNotFoundException("Recipe not found");

            ladleMeThisContext.Entry(existingRecipe).CurrentValues.SetValues(recipe);
            await ladleMeThisContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating recipe: {ex.Message}");
            throw new InvalidOperationException("An error occurred while updating the recipe");
        }
    }

    public async Task<bool> Delete(int recipeId)
    {
        try
        {
            var recipe = await GetByRecipeId(recipeId);
            if (recipe == null) throw new KeyNotFoundException("Recipe not found");

            ladleMeThisContext.Recipes.Remove(recipe);
            await ladleMeThisContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting recipe: {ex.Message}");
            throw new InvalidOperationException("An error occurred while deleting the recipe");
        }
    }

    public async Task<List<Recipe>> GetByName(string name)
    {
        return await ladleMeThisContext.Recipes
            .Include(r => r.Categories)
            .Include(r => r.Tags)
            .Include(r => r.Ingredients)
            .Include(r => r.Ratings)
            .Include(r => r.User)
            .Include(r => r.RecipeImage)
            .Where(r => r.Name.ToLower().Contains(name.ToLower()))
            .ToListAsync();

    }

    public async Task<List<Recipe>> GetByCategoryIdAndName(int categoryId, string recipeName)
    {
        return await ladleMeThisContext.Recipes
            .Include(r => r.Categories)
            .Include(r => r.Tags)
            .Include(r => r.Ingredients)
            .Include(r => r.Ratings)
            .Include(r => r.User)
            .Include(r => r.RecipeImage)
            .Where(r => r.Name.ToLower().Contains(recipeName.ToLower()) && r.Categories.Any(c => c.CategoryId == categoryId))
            .ToListAsync();
    }
}