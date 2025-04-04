using LadleMeThis.Data.Entity;
using LadleMeThis.Models.CategoryModels;
using LadleMeThis.Repositories.CategoryRepository;
using LadleMeThis.Services.CategoryService;
using Moq;

namespace LadleMeThisTests;

public class CategoryServiceTests
{
	private Mock<ICategoryRepository> _categoryRepositoryMock;
	private CategoryService _categoryService;

	[SetUp]
	public void Setup()
	{
		_categoryRepositoryMock = new Mock<ICategoryRepository>();
		_categoryService = new CategoryService(_categoryRepositoryMock.Object);
	}

	[Test]
	public async Task AddAsync_ShouldReturnCategoryDTO_WhenCategoryIsAdded()
	{
		var categoryCreateRequest = new CategoryCreateRequest { Name = "New Category" };
		var categoryEntity = new Category { CategoryId = 1, Name = categoryCreateRequest.Name };

		_categoryRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Category>())).ReturnsAsync(categoryEntity);

		var result = await _categoryService.AddAsync(categoryCreateRequest);

		Assert.That(result, Is.Not.Null);
		Assert.Multiple(() =>
		{
			Assert.That(result.Name, Is.EqualTo("New Category"));
			Assert.That(result.CategoryId, Is.EqualTo(1));
		});
	}

	[Test]
	public async Task DeleteByIdAsync_ShouldCallDeleteMethodOnce()
	{
		const int categoryId = 1;
		_categoryRepositoryMock.Setup(repo => repo.DeleteByIdAsync(categoryId)).Returns(Task.CompletedTask);

		await _categoryService.DeleteByIdAsync(categoryId);

		_categoryRepositoryMock.Verify(repo => repo.DeleteByIdAsync(categoryId), Times.Once);
	}

	[Test]
	public async Task GetAllAsync_ShouldReturnCategoryDTOs_WhenCategoriesExist()
	{
		var categories = new List<Category>
		{
			new Category { CategoryId = 1, Name = "Category 1" },
			new Category { CategoryId = 2, Name = "Category 2" }
		};
		_categoryRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(categories);

		var result = await _categoryService.GetAllAsync();
		var resultList = result.ToList();

		Assert.Multiple(() =>
		{
			Assert.That(resultList, Has.Count.EqualTo(2));
			Assert.That(resultList.First().Name, Is.EqualTo("Category 1"));
		});
	}

	[Test]
	public async Task GetByIdAsync_ShouldReturnCategoryDTO_WhenCategoryExists()
	{
		var categoryId = 1;
		var category = new Category { CategoryId = categoryId, Name = "Category 1" };
		_categoryRepositoryMock.Setup(repo => repo.GetByIdAsync(categoryId)).ReturnsAsync(category);

		var result = await _categoryService.GetByIdAsync(categoryId);

		Assert.Multiple(() =>
		{
			Assert.That(result.Name, Is.EqualTo("Category 1"));
			Assert.That(result.CategoryId, Is.EqualTo(categoryId));
		});
	}

	[Test]
	public async Task GetManyByIdAsync_ShouldReturnMultipleCategoryDTOs_WhenCategoriesExist()
	{
		var categoryIds = new[] { 1, 2 };
		var categories = new List<Category>
		{
			new Category { CategoryId = 1, Name = "Category 1" },
			new Category { CategoryId = 2, Name = "Category 2" }
		};
		_categoryRepositoryMock.Setup(repo => repo.GetManyByIdAsync(categoryIds)).ReturnsAsync(categories);

		var result = await _categoryService.GetManyByIdAsync(categoryIds);
		var resultList = result.ToList();

		Assert.Multiple(() =>
		{
			Assert.That(resultList, Has.Count.EqualTo(2));
			Assert.That(resultList.First().Name, Is.EqualTo("Category 1"));
			Assert.That(resultList.Last().Name, Is.EqualTo("Category 2"));
		});
	}
}