using LadleMeThis.Data.Entity;
using LadleMeThis.Models.IngredientsModels;
using LadleMeThis.Models.RecipeRatingsModels;
using LadleMeThis.Models.TagModels;
using LadleMeThis.Models.UserModels;
using LadleMeThis.Repositories.CategoryRepository;
using LadleMeThis.Repositories.IngredientRepository;
using LadleMeThis.Repositories.TagRepository;
using LadleMeThis.Services.RecipeDetailService;
using LadleMeThis.Services.RecipeRatingService;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace LadleMeThisTests;

public class RecipeDetailServiceTests
{
	private Mock<ITagRepository> _mockTagRepo;
        private Mock<IIngredientRepository> _mockIngredientRepo;
        private Mock<ICategoryRepository> _mockCategoryRepo;
        private Mock<IRecipeRatingService> _mockRecipeRatingService;
        private RecipeDetailService _recipeDetailService;

        [SetUp]
        public void SetUp()
        {
            _mockTagRepo = new Mock<ITagRepository>();
            _mockIngredientRepo = new Mock<IIngredientRepository>();
            _mockCategoryRepo = new Mock<ICategoryRepository>();
            _mockRecipeRatingService = new Mock<IRecipeRatingService>();
            
            _recipeDetailService = new RecipeDetailService(
                _mockTagRepo.Object,
                _mockIngredientRepo.Object,
                _mockCategoryRepo.Object,
                _mockRecipeRatingService.Object
            );
        }

        [Test]
        public async Task GetTagsByIds_ShouldReturnTags_WhenTagIdsAreProvided()
        {
            
            var tagIds = new int[] { 1, 2 };
            var expectedTags = new List<Tag>
            {
                new Tag { TagId = 1, Name = "Vegan" },
                new Tag { TagId = 2, Name = "Gluten-Free" }
            };

            _mockTagRepo.Setup(repo => repo.GetManyByIdAsync(tagIds)).ReturnsAsync(expectedTags);
            
            var result = await _recipeDetailService.GetTagsByIds(tagIds);

            Assert.That(result, Is.EqualTo(expectedTags));
        }

        [Test]
        public async Task GetIngredientsByIds_ShouldReturnIngredients_WhenIngredientIdsAreProvided()
        {
            var ingredientIds = new int[] { 1, 2 };
            var expectedIngredients = new List<Ingredient>
            {
                new Ingredient { IngredientId = 1, Name = "Salt", Unit = "tsp" },
                new Ingredient { IngredientId = 2, Name = "Pepper", Unit = "tsp" }
            };

            _mockIngredientRepo.Setup(repo => repo.GetManyByIdAsync(ingredientIds)).ReturnsAsync(expectedIngredients);
            
            var result = await _recipeDetailService.GetIngredientsByIds(ingredientIds);

            Assert.That(result, Is.EqualTo(expectedIngredients));
        }

        [Test]
        public async Task GetCategoriesByIds_ShouldReturnCategories_WhenCategoryIdsAreProvided()
        {
            var categoryIds = new int[] { 1, 2 };
            var expectedCategories = new List<Category>
            {
                new Category { CategoryId = 1, Name = "Dessert" },
                new Category { CategoryId = 2, Name = "Main Course" }
            };

            _mockCategoryRepo.Setup(repo => repo.GetManyByIdAsync(categoryIds)).ReturnsAsync(expectedCategories);
            
            var result = await _recipeDetailService.GetCategoriesByIds(categoryIds);
            
            Assert.That(result, Is.EqualTo(expectedCategories));
        }

        [Test]
        public void GetTagDTOsByTags_ShouldReturnTagDTOs_WhenTagsAreProvided()
        {
            var tags = new List<Tag>
            {
                new Tag { TagId = 1, Name = "Vegan" },
                new Tag { TagId = 2, Name = "Gluten-Free" }
            };

            var expectedTagDTOs = tags.Select(tag => new TagDTO { TagId = tag.TagId, Name = tag.Name }).ToList();
            
            var result = _recipeDetailService.GetTagDTOsByTags(tags).ToList();
            
            Assert.Multiple(() =>
            {
                Assert.That(result.First().TagId, Is.EqualTo(expectedTagDTOs.First().TagId));
                Assert.That(result.First().Name, Is.EqualTo(expectedTagDTOs.First().Name));

                Assert.That(result.Last().TagId, Is.EqualTo(expectedTagDTOs.Last().TagId));
                Assert.That(result.Last().Name, Is.EqualTo(expectedTagDTOs.Last().Name));
            });
        }

        [Test]
        public void GetIngredientDTOsByIngredients_ShouldReturnIngredientDTOs_WhenIngredientsAreProvided()
        {
            var ingredients = new List<Ingredient>
            {
                new Ingredient { IngredientId = 1, Name = "Salt", Unit = "tsp" },
                new Ingredient { IngredientId = 2, Name = "Pepper", Unit = "tsp" }
            };

            var expectedIngredientDTOs = ingredients.Select(ingredient => new IngredientDTO
            {
                IngredientId = ingredient.IngredientId,
                Name = ingredient.Name,
                Unit = ingredient.Unit
            }).ToList();
            
            var result = _recipeDetailService.GetIngredientDTOsByIngredients(ingredients).ToList();
            
            Assert.Multiple(() =>
            {
                Assert.That(result.First().IngredientId, Is.EqualTo(expectedIngredientDTOs.First().IngredientId));
                Assert.That(result.First().Name, Is.EqualTo(expectedIngredientDTOs.First().Name));
                Assert.That(result.First().Unit, Is.EqualTo(expectedIngredientDTOs.First().Unit));

                Assert.That(result.Last().IngredientId, Is.EqualTo(expectedIngredientDTOs.Last().IngredientId));
                Assert.That(result.Last().Name, Is.EqualTo(expectedIngredientDTOs.Last().Name));
                Assert.That(result.Last().Unit, Is.EqualTo(expectedIngredientDTOs.Last().Unit));
            });
        }

        [Test]
        public void CreateRecipeRatingDtoList_ShouldReturnListOfRecipeRatingDTO_WhenRatingsAreProvided()
        {
            var ratings = new List<RecipeRating>
            {
                new RecipeRating
                {
                    RatingId = 1,
                    Review = "Great recipe!",
                    Rating = 5,
                    DateCreated = DateTime.UtcNow,
                    User = new IdentityUser { Id = "user1" }
                }
            };

            var expectedRecipeRatingDTOs = new List<RecipeRatingDTO>
            {
                new RecipeRatingDTO
                (
                    1, "Great recipe!", 5, DateTime.UtcNow, 
                    new UserReviewDTO { UserId = "user1", UserName = "John Doe" }
                )
            };

            _mockRecipeRatingService.Setup(service => service.CreateRecipeRatingDtoList(It.IsAny<IEnumerable<RecipeRating>>()))
                .ReturnsAsync(expectedRecipeRatingDTOs);
            
            var result = _recipeDetailService.CreateRecipeRatingDtoList(ratings);
            
            Assert.That(result, Is.EqualTo(expectedRecipeRatingDTOs));
        }
}