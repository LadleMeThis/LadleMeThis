using LadleMeThis.Models.CategoryModels;
using LadleMeThis.Models.IngredientsModels;
using LadleMeThis.Repositories.IngredientRepository;

namespace LadleMeThis.Services.IngredientService
{
    //note for self : make a parser method DTO -> model, model -> DTO

    public class IngredientService : IIngredientService
    {
        IIngredientRepository _ingredientRepository;
        public IngredientService(IIngredientRepository ingredientRepo)
        {
            _ingredientRepository = ingredientRepo;
        }


        public async Task<IngredientDTO> AddAsync(IngredientCreateRequest ingredientCreateRequest)
        {
            var ingredient = await _ingredientRepository.AddAsync(new Ingredient() { Name = ingredientCreateRequest.Name, Unit = ingredientCreateRequest.Unit });
            return new IngredientDTO() { Name = ingredient.Name, Unit = ingredient.Unit, IngredientId = ingredient.IngredientId };
        }

        public async Task DeleteByIdAsync(int ingredientId)
        {
            await _ingredientRepository.DeleteByIdAsync(ingredientId);
        }

        public async Task<IEnumerable<IngredientDTO>> GetAllAsync()
        {
            var ingredients = await _ingredientRepository.GetAllAsync();
            return ingredients.Select(ingredient => new IngredientDTO() { Name = ingredient.Name, Unit = ingredient.Unit }).ToList();
        }

        public async Task<IngredientDTO?> GetByIdAsync(int ingredientId)
        {
            var ingredient = await _ingredientRepository.GetByIdAsync(ingredientId);
            return new IngredientDTO() { Name = ingredient.Name, Unit = ingredient.Unit };
        }

        public async Task<IEnumerable<IngredientDTO>> GetManyByIdAsync(int[] ingredientIds)
        {
            var ingredients = await _ingredientRepository.GetManyByIdAsync(ingredientIds);
            return ingredients.Select(ingredient => new IngredientDTO() { Name = ingredient.Name, Unit = ingredient.Unit }).ToList();
        }
    }
}
