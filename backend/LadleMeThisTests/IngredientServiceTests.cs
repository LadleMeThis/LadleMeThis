using LadleMeThis.Data.Entity;
using LadleMeThis.Models.IngredientsModels;
using LadleMeThis.Repositories.IngredientRepository;
using LadleMeThis.Services.IngredientService;
using Moq;

namespace LadleMeThisTests;

public class IngredientServiceTests
{
	private Mock<IIngredientRepository> _ingredientRepositoryMock;
	private IngredientService _ingredientService;

	[SetUp]
	public void Setup()
	{
		_ingredientRepositoryMock = new Mock<IIngredientRepository>();
		_ingredientService = new IngredientService(_ingredientRepositoryMock.Object);
	}

	[Test]
	public async Task AddAsync_ShouldReturnIngredientDTO_WhenIngredientIsAdded()
	{
		var ingredientCreateRequest = new IngredientCreateRequest { Name = "New Ingredient", Unit = "kg" };
		var ingredientEntity = new Ingredient
			{ IngredientId = 1, Name = ingredientCreateRequest.Name, Unit = ingredientCreateRequest.Unit };

		_ingredientRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Ingredient>())).ReturnsAsync(ingredientEntity);

		var result = await _ingredientService.AddAsync(ingredientCreateRequest);

		Assert.That(result, Is.Not.Null);
		Assert.Multiple(() =>
		{
			Assert.That(result.Name, Is.EqualTo("New Ingredient"));
			Assert.That(result.Unit, Is.EqualTo("kg"));
			Assert.That(result.IngredientId, Is.EqualTo(1));
		});
	}

	[Test]
	public async Task DeleteByIdAsync_ShouldCallDeleteMethodOnce()
	{
		const int ingredientId = 1;
		_ingredientRepositoryMock.Setup(repo => repo.DeleteByIdAsync(ingredientId)).Returns(Task.CompletedTask);

		await _ingredientService.DeleteByIdAsync(ingredientId);

		_ingredientRepositoryMock.Verify(repo => repo.DeleteByIdAsync(ingredientId), Times.Once);
	}

	[Test]
	public async Task GetAllAsync_ShouldReturnIngredientDTOs_WhenIngredientsExist()
	{
		var ingredients = new List<Ingredient>
		{
			new Ingredient { IngredientId = 1, Name = "Ingredient 1", Unit = "kg" },
			new Ingredient { IngredientId = 2, Name = "Ingredient 2", Unit = "g" }
		};
		_ingredientRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(ingredients);

		var result = await _ingredientService.GetAllAsync();
		var resultList = result.ToList();

		Assert.Multiple(() =>
		{
			Assert.That(resultList, Has.Count.EqualTo(2));
			Assert.That(resultList.First().Name, Is.EqualTo("Ingredient 1"));
			Assert.That(resultList.First().Unit, Is.EqualTo("kg"));
		});
	}

	[Test]
	public async Task GetByIdAsync_ShouldReturnIngredientDTO_WhenIngredientExists()
	{
		const int ingredientId = 1;
		var ingredient = new Ingredient { IngredientId = ingredientId, Name = "Ingredient 1", Unit = "kg" };
		_ingredientRepositoryMock.Setup(repo => repo.GetByIdAsync(ingredientId)).ReturnsAsync(ingredient);

		var result = await _ingredientService.GetByIdAsync(ingredientId);

		Assert.Multiple(() =>
		{
			Assert.That(result.Name, Is.EqualTo("Ingredient 1"));
			Assert.That(result.Unit, Is.EqualTo("kg"));
			Assert.That(result.IngredientId, Is.EqualTo(ingredientId));
		});
	}

	[Test]
	public async Task GetManyByIdAsync_ShouldReturnMultipleIngredientDTOs_WhenIngredientsExist()
	{
		var ingredientIds = new[] { 1, 2 };
		var ingredients = new List<Ingredient>
		{
			new Ingredient { IngredientId = 1, Name = "Ingredient 1", Unit = "kg" },
			new Ingredient { IngredientId = 2, Name = "Ingredient 2", Unit = "g" }
		};
		_ingredientRepositoryMock.Setup(repo => repo.GetManyByIdAsync(ingredientIds)).ReturnsAsync(ingredients);

		var result = await _ingredientService.GetManyByIdAsync(ingredientIds);
		var resultList = result.ToList();

		Assert.Multiple(() =>
		{
			Assert.That(resultList, Has.Count.EqualTo(2));
			Assert.That(resultList.First().Name, Is.EqualTo("Ingredient 1"));
			Assert.That(resultList.Last().Name, Is.EqualTo("Ingredient 2"));
		});
	}
}