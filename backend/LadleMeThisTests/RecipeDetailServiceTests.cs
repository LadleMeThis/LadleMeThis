using LadleMeThis.Data.Entity;
using LadleMeThis.Models.CategoryModels;
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
        
        [Test]
        public void GetCategoryDTOsByCategories_ShouldReturnCategoryDTOs_WhenCategoriesAreProvided()
        {
            var categories = new List<Category>
            {
                new Category { CategoryId = 1, Name = "Dessert" },
                new Category { CategoryId = 2, Name = "Appetizer" }
            };
            var expectedCategoryDTOs = new List<CategoryDTO>
            {
                new CategoryDTO { CategoryId = 1, Name = "Dessert" },
                new CategoryDTO { CategoryId = 2, Name = "Appetizer" }
            };
            
            var result = _recipeDetailService.GetCategoryDTOsByCategories(categories).ToList();
            
            Assert.Multiple(() =>
            {
                Assert.That(result.First().CategoryId, Is.EqualTo(expectedCategoryDTOs.First().CategoryId));
                Assert.That(result.First().Name, Is.EqualTo(expectedCategoryDTOs.First().Name));

                Assert.That(result.Last().CategoryId, Is.EqualTo(expectedCategoryDTOs.Last().CategoryId));
                Assert.That(result.Last().Name, Is.EqualTo(expectedCategoryDTOs.Last().Name));
            });
        }
        
        [Test]
        public void GetCategoryDTOsByCategories_ShouldReturnEmptyList_WhenCategoriesAreEmpty()
        {
            var categories = new List<Category>();

            var result = _recipeDetailService.GetCategoryDTOsByCategories(categories);

            Assert.That(result, Is.Empty);
        }
        
        [Test]
        public void GetCategoryDTOsByCategories_ShouldThrowArgumentNullException_WhenCategoriesAreNull()
        {
            List<Category> categories = null;

            Assert.Throws<ArgumentNullException>(() => _recipeDetailService.GetCategoryDTOsByCategories(categories));
        }
        
        [Test]
        public void GetCategoryDTOsByCategories_ShouldHandleDuplicateCategories()
        {
            var categories = new List<Category>
            {
                new Category { CategoryId = 1, Name = "Dessert" },
                new Category { CategoryId = 1, Name = "Dessert" }
            };

            var result = _recipeDetailService.GetCategoryDTOsByCategories(categories).ToList();

            Assert.Multiple(() =>
            {
                Assert.That(result.Count, Is.EqualTo(2));
                Assert.That(result[0].CategoryId, Is.EqualTo(1));
                Assert.That(result[0].Name, Is.EqualTo("Dessert"));
                Assert.That(result[1].CategoryId, Is.EqualTo(1));
                Assert.That(result[1].Name, Is.EqualTo("Dessert"));
            });
        }
        
        [Test]
        public void GetCategoryDTOsByCategories_ShouldHandleCategoriesWithNullProperties()
        {
            var categories = new List<Category>
            {
                new Category { CategoryId = 1, Name = null },
                new Category { CategoryId = 2, Name = "Appetizer" }
            };

            var result = _recipeDetailService.GetCategoryDTOsByCategories(categories).ToList();

            Assert.Multiple(() =>
            {
                Assert.That(result[0].CategoryId, Is.EqualTo(1));
                Assert.That(result[0].Name, Is.Null);

                Assert.That(result[1].CategoryId, Is.EqualTo(2));
                Assert.That(result[1].Name, Is.EqualTo("Appetizer"));
            });
        }
        
        [Test]
        public async Task AddRecipeRatingAsync_ShouldCallCreateRecipeRating_AndReturnRatingId_WhenValidDataIsProvided()
        {
            var recipeRatingDto = new CreateRecipeRatingDTO(5, "Delicious!");
            var user = new IdentityUser { Id = "user1", UserName = "JohnDoe" };
            var recipe = new Recipe { RecipeId = 1, Name = "Pasta" };

            _mockRecipeRatingService
                .Setup(service => service.CreateRecipeRating(recipeRatingDto, user, recipe))
                .ReturnsAsync(1);

            var result = await _recipeDetailService.AddRecipeRatingAsync(recipeRatingDto, user, recipe);

            Assert.That(result, Is.EqualTo(1));
            _mockRecipeRatingService.Verify(service => service.CreateRecipeRating(recipeRatingDto, user, recipe), Times.Once);
        }
        

        
        [Test]
        public async Task AddRecipeRatingAsync_ShouldThrowException_WhenRecipeRatingServiceFails()
        {
            var recipeRatingDto = new CreateRecipeRatingDTO(5, "Delicious!");
            var user = new IdentityUser { Id = "user1", UserName = "JohnDoe" };
            var recipe = new Recipe { RecipeId = 1, Name = "Pasta" };

            _mockRecipeRatingService
                .Setup(service => service.CreateRecipeRating(recipeRatingDto, user, recipe))
                .ThrowsAsync(new Exception("Service failed."));

            var ex = Assert.ThrowsAsync<Exception>(() => _recipeDetailService.AddRecipeRatingAsync(recipeRatingDto, user, recipe));
            Assert.That(ex.Message, Is.EqualTo("Service failed."));
        }
        
        [Test]
        public async Task GetTagsByIds_ShouldReturnEmptyList_WhenNoTagIdsAreProvided()
        {
            var tagIds = new int[] { };
            var expectedTags = new List<Tag>();

            _mockTagRepo.Setup(repo => repo.GetManyByIdAsync(tagIds)).ReturnsAsync(expectedTags);
    
            var result = await _recipeDetailService.GetTagsByIds(tagIds);

            Assert.That(result, Is.EqualTo(expectedTags));
        }
        
        [Test]
        public async Task GetIngredientsByIds_ShouldReturnEmptyList_WhenNoIngredientIdsAreProvided()
        {
            var ingredientIds = new int[] { };
            var expectedIngredients = new List<Ingredient>();

            _mockIngredientRepo.Setup(repo => repo.GetManyByIdAsync(ingredientIds)).ReturnsAsync(expectedIngredients);
    
            var result = await _recipeDetailService.GetIngredientsByIds(ingredientIds);

            Assert.That(result, Is.EqualTo(expectedIngredients));
        }
        
        [Test]
        public async Task GetCategoriesByIds_ShouldReturnEmptyList_WhenNoCategoryIdsAreProvided()
        {
            var categoryIds = new int[] { };
            var expectedCategories = new List<Category>();

            _mockCategoryRepo.Setup(repo => repo.GetManyByIdAsync(categoryIds)).ReturnsAsync(expectedCategories);
    
            var result = await _recipeDetailService.GetCategoriesByIds(categoryIds);

            Assert.That(result, Is.EqualTo(expectedCategories));
        }
        
        [Test]
        public void GetTagDTOsByTags_ShouldReturnEmptyList_WhenTagsAreNullOrEmpty()
        {
            var tags = new List<Tag>();

            var result = _recipeDetailService.GetTagDTOsByTags(tags).ToList();

            Assert.That(result, Is.Empty);
        }
        
        [Test]
        public void GetIngredientDTOsByIngredients_ShouldReturnEmptyList_WhenIngredientsAreNullOrEmpty()
        {
            var ingredients = new List<Ingredient>();

            var result = _recipeDetailService.GetIngredientDTOsByIngredients(ingredients).ToList();

            Assert.That(result, Is.Empty);
        }
        
        [Test]
        public void GetTagDTOsByTags_ShouldHandleTagsWithMissingNameOrId()
        {
            var tags = new List<Tag>
            {
                new Tag { TagId = 1, Name = null },
                new Tag { TagId = 2, Name = "Gluten-Free" }
            };

            var result = _recipeDetailService.GetTagDTOsByTags(tags).ToList();

            Assert.Multiple(() =>
            {
                Assert.That(result[0].TagId, Is.EqualTo(1));
                Assert.That(result[0].Name, Is.Null);
                Assert.That(result[1].TagId, Is.EqualTo(2));
                Assert.That(result[1].Name, Is.EqualTo("Gluten-Free"));
            });
        }
        
        [Test]
        public void GetIngredientDTOsByIngredients_ShouldHandleIngredientsWithMissingUnit()
        {
            var ingredients = new List<Ingredient>
            {
                new Ingredient { IngredientId = 1, Name = "Salt", Unit = null },
                new Ingredient { IngredientId = 2, Name = "Pepper", Unit = "tsp" }
            };

            var result = _recipeDetailService.GetIngredientDTOsByIngredients(ingredients).ToList();

            Assert.Multiple(() =>
            {
                Assert.That(result[0].IngredientId, Is.EqualTo(1));
                Assert.That(result[0].Unit, Is.Null);
                Assert.That(result[1].IngredientId, Is.EqualTo(2));
                Assert.That(result[1].Unit, Is.EqualTo("tsp"));
            });
        }
    





}