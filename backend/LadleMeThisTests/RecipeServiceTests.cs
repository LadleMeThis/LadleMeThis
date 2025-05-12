using LadleMeThis.Data.Entity;
using LadleMeThis.Models.CategoryModels;
using LadleMeThis.Models.RecipeModels;
using LadleMeThis.Models.TagModels;
using LadleMeThis.Repositories.RecipeRepository;
using LadleMeThis.Services.RecipeDetailService;
using LadleMeThis.Services.RecipeService;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace LadleMeThisTests
{

    [TestFixture]
    public class RecipeServiceTests
    {
        private Mock<IRecipeRepository> _recipeRepoMock;
        private Mock<IRecipeDetailService> _recipeDetailServiceMock;
        private Mock<UserManager<IdentityUser>> _userManagerMock;
        private RecipeService _service;

        [SetUp]
        public void Setup()
        {
            _recipeRepoMock = new Mock<IRecipeRepository>();
            _recipeDetailServiceMock = new Mock<IRecipeDetailService>();

            var store = new Mock<IUserStore<IdentityUser>>();
            _userManagerMock = new Mock<UserManager<IdentityUser>>(
                store.Object, null, null, null, null, null, null, null, null);

            _service = new RecipeService(_recipeRepoMock.Object, _recipeDetailServiceMock.Object, _userManagerMock.Object);
        }

        [Test]
        public async Task GetAllRecipeCards_ReturnsMappedRecipes()
        {
            var recipes = new List<Recipe>
        {
            new() { Name = "Recipe", CookTime = 10, PrepTime = 5, ServingSize = 2, Tags = [], Categories = [], Ratings = [], RecipeImage = new RecipeImage() }
        };
            _recipeRepoMock.Setup(r => r.GetAll()).ReturnsAsync(recipes);
            _recipeDetailServiceMock.Setup(r => r.GetTagDTOsByTags(It.IsAny<ICollection<Tag>>())).Returns(new List<TagDTO>());
            _recipeDetailServiceMock.Setup(r => r.GetCategoryDTOsByCategories(It.IsAny<ICollection<Category>>())).Returns(new List<CategoryDTO>());

            var result = await _service.GetAllRecipeCards();

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("Recipe"));
        }

        [Test]
        public async Task GetRecipesByName_ReturnsMappedRecipes()
        {
            var recipes = new List<Recipe>
        {
            new() { Name = "Recipe", CookTime = 10, PrepTime = 5, ServingSize = 2, Tags = [], Categories = [], Ratings = [], RecipeImage = new RecipeImage() }
        };
            _recipeRepoMock.Setup(r => r.GetByName("Recipe")).ReturnsAsync(recipes);
            _recipeDetailServiceMock.Setup(r => r.GetTagDTOsByTags(It.IsAny<ICollection<Tag>>())).Returns(new List<TagDTO>());
            _recipeDetailServiceMock.Setup(r => r.GetCategoryDTOsByCategories(It.IsAny<ICollection<Category>>())).Returns(new List<CategoryDTO>());

            var result = await _service.GetRecipesByName("Recipe");

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("Recipe"));
        }

        [Test]
        public async Task GetRecipesByCategoryId_ReturnsMappedRecipes()
        {
            var recipes = new List<Recipe>
        {
            new() { Name = "Recipe", CookTime = 10, PrepTime = 5, ServingSize = 2, Tags = [], Categories = [], Ratings = [], RecipeImage = new RecipeImage() }
        };
            _recipeRepoMock.Setup(r => r.GetByCategoryId(1)).ReturnsAsync(recipes);
            _recipeDetailServiceMock.Setup(r => r.GetTagDTOsByTags(It.IsAny<ICollection<Tag>>())).Returns(new List<TagDTO>());
            _recipeDetailServiceMock.Setup(r => r.GetCategoryDTOsByCategories(It.IsAny<ICollection<Category>>())).Returns(new List<CategoryDTO>());

            var result = await _service.GetRecipesByCategoryId(1);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("Recipe"));
        }

        [Test]
        public async Task GetRecipesByTagId_ReturnsMappedRecipes()
        {
            var recipes = new List<Recipe>
        {
            new() { Name = "Recipe", CookTime = 10, PrepTime = 5, ServingSize = 2, Tags = [], Categories = [], Ratings = [], RecipeImage = new RecipeImage() }
        };
            _recipeRepoMock.Setup(r => r.GetByTagId(1)).ReturnsAsync(recipes);
            _recipeDetailServiceMock.Setup(r => r.GetTagDTOsByTags(It.IsAny<ICollection<Tag>>())).Returns(new List<TagDTO>());
            _recipeDetailServiceMock.Setup(r => r.GetCategoryDTOsByCategories(It.IsAny<ICollection<Category>>())).Returns(new List<CategoryDTO>());

            var result = await _service.GetRecipesByTagId(1);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("Recipe"));
        }

        [Test]
        public async Task CreateRecipe_CreatesAndReturnsRecipeId()
        {
            var createRecipeDto = new CreateRecipeDTO("Recipe", 10, 5, "instructions", 3, new int[1], new int[1], new int[1]);
            var userId = "user123";
            _userManagerMock.Setup(u => u.FindByIdAsync(userId)).ReturnsAsync(new IdentityUser { UserName = "user" });
            _recipeRepoMock.Setup(r => r.Create(It.IsAny<Recipe>())).ReturnsAsync(1);

            var result = await _service.Create(createRecipeDto, userId);

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public async Task UpdateRecipe_UpdatesAndReturnsTrue()
        {
            var updateRecipeDto = new UpdateRecipeDTO(1, "Updated Recipe", 10, 5, "instructions", 3, new int[1], new int[1], new int[1]);
            var recipe = new Recipe { RecipeId = 1, Name = "Old Recipe" };
            _recipeRepoMock.Setup(r => r.GetByRecipeId(1)).ReturnsAsync(recipe);
            _recipeRepoMock.Setup(r => r.Update(It.IsAny<Recipe>())).ReturnsAsync(true);

            var result = await _service.UpdateRecipe(updateRecipeDto);

            Assert.That(result, Is.True);
            Assert.That(recipe.Name, Is.EqualTo("Updated Recipe"));
        }

        [Test]
        public async Task DeleteRecipe_ReturnsTrueOnSuccess()
        {
            _recipeRepoMock.Setup(r => r.Delete(1)).ReturnsAsync(true);

            var result = await _service.DeleteRecipe(1);

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task GetRecipeByRecipeId_ReturnsMappedRecipe()
        {
            var recipe = new Recipe { RecipeId = 1, Name = "Recipe" , User = new IdentityUser()};
            _recipeRepoMock.Setup(r => r.GetByRecipeId(1)).ReturnsAsync(recipe);

            var result = await _service.GetRecipeByRecipeId(1);

            Assert.That(result.RecipeId, Is.EqualTo(1));
            Assert.That(result.Name, Is.EqualTo("Recipe"));
        }

        [Test]
        public async Task GetRecipesByCategoryIdAndName_ReturnsMappedRecipes()
        {
            var recipes = new List<Recipe>
        {
            new() { Name = "Recipe", CookTime = 10, PrepTime = 5, ServingSize = 2, Tags = [], Categories = [], Ratings = [], RecipeImage = new RecipeImage() }
        };
            _recipeRepoMock.Setup(r => r.GetByCategoryIdAndName(1, "Recipe")).ReturnsAsync(recipes);
            _recipeDetailServiceMock.Setup(r => r.GetTagDTOsByTags(It.IsAny<ICollection<Tag>>())).Returns(new List<TagDTO>());
            _recipeDetailServiceMock.Setup(r => r.GetCategoryDTOsByCategories(It.IsAny<ICollection<Category>>())).Returns(new List<CategoryDTO>());

            var result = await _service.GetRecipesByCategoryIdAndName(1, "Recipe");

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("Recipe"));
        }

        [Test]
        public async Task GetRecipesByUserId_ReturnsMappedRecipes()
        {
            var recipes = new List<Recipe>
        {
            new() { Name = "Recipe", CookTime = 10, PrepTime = 5, ServingSize = 2, Tags = [], Categories = [], Ratings = [], RecipeImage = new RecipeImage() }
        };
            _recipeRepoMock.Setup(r => r.GetByUserId("user123")).ReturnsAsync(recipes);
            _recipeDetailServiceMock.Setup(r => r.GetTagDTOsByTags(It.IsAny<ICollection<Tag>>())).Returns(new List<TagDTO>());
            _recipeDetailServiceMock.Setup(r => r.GetCategoryDTOsByCategories(It.IsAny<ICollection<Category>>())).Returns(new List<CategoryDTO>());

            var result = await _service.GetRecipesByUserId("user123");

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("Recipe"));
        }
    }
}
