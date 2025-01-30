﻿using LadleMeThis.Models.CategoryModels;
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


        //tag class alapján visszaad egy DTO-t (paraméter Icollection) ICollection<Tag>

        public async Task<IEnumerable<Tag>> GetTagsByIds(int[] tagIds)
        {
            return await _tagRepository.GetManyByIdAsync(tagIds);
        }

        public async Task<IEnumerable<Category>> GetCategoriesByIds(int[] categoryIds)
        {
            return await _categoryRepository.GetManyByIdAsync(categoryIds);
        }

        public async Task<IEnumerable<Ingredient>> GetIngredientsByIds(int[] ingredientIds)
        {
            return await _ingredientRepository.GetManyByIdAsync(ingredientIds);
        }

    }
}
