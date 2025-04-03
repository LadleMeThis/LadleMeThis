using LadleMeThis.Data.Entity;
using LadleMeThis.Models.TagModels;
using LadleMeThis.Repositories.TagRepository;
using LadleMeThis.Services.TagService;
using Moq;

namespace LadleMeThisTests;

public class TagServiceTests
{
	private Mock<ITagRepository> _tagRepositoryMock;
	private TagService _tagService;

	[SetUp]
	public void Setup()
	{
		_tagRepositoryMock = new Mock<ITagRepository>();
		_tagService = new TagService(_tagRepositoryMock.Object);
	}

	[Test]
	public async Task AddAsync_ShouldReturnTagDTO_WhenTagIsAdded()
	{
		var tagCreateRequest = new TagCreateRequest { Name = "New Tag" };
		var tagEntity = new Tag { TagId = 1, Name = tagCreateRequest.Name };

		_tagRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Tag>())).ReturnsAsync(tagEntity);
		
		var result = await _tagService.AddAsync(tagCreateRequest);
		
		Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Name, Is.EqualTo("New Tag"));
            Assert.That(result.TagId, Is.EqualTo(1));
        });
    }

	[Test]
	public async Task DeleteByIdAsync_ShouldCallDeleteMethodOnce()
	{
		const int tagId = 1;
		_tagRepositoryMock.Setup(repo => repo.DeleteByIdAsync(tagId)).Returns(Task.CompletedTask);
		
		await _tagService.DeleteByIdAsync(tagId);
		
		_tagRepositoryMock.Verify(repo => repo.DeleteByIdAsync(tagId), Times.Once);
	}

	[Test]
	public async Task GetAllAsync_ShouldReturnTagDTOs_WhenTagsExist()
	{
		var tags = new List<Tag>
		{
			new Tag { TagId = 1, Name = "Tag 1" },
			new Tag { TagId = 2, Name = "Tag 2" }
		};
		_tagRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(tags);
		
		var result = await _tagService.GetAllAsync();
		var resultList = result.ToList();

        Assert.Multiple(() =>
        {
            Assert.That(resultList, Has.Count.EqualTo(2));
            Assert.That(resultList.First().Name, Is.EqualTo("Tag 1"));
        });
    }

	[Test]
	public async Task GetByIdAsync_ShouldReturnTagDTO_WhenTagExists()
	{
		var tagId = 1;
		var tag = new Tag { TagId = tagId, Name = "Tag 1" };
		_tagRepositoryMock.Setup(repo => repo.GetByIdAsync(tagId)).ReturnsAsync(tag);

		var result = await _tagService.GetByIdAsync(tagId);

		Assert.Multiple(() =>
		{
			Assert.That(result.Name, Is.EqualTo("Tag 1"));
			Assert.That(result.TagId, Is.EqualTo(tagId));
		});
	}

	[Test]
	public async Task GetManyByIdAsync_ShouldReturnMultipleTagDTOs_WhenTagsExist()
	{
		// Arrange
		var tagIds = new[] { 1, 2 };
		var tags = new List<Tag>
		{
			new Tag { TagId = 1, Name = "Tag 1" },
			new Tag { TagId = 2, Name = "Tag 2" }
		};
		_tagRepositoryMock.Setup(repo => repo.GetManyByIdAsync(tagIds)).ReturnsAsync(tags);


		var result = await _tagService.GetManyByIdAsync(tagIds);
		var resultList = result.ToList();
		
		Assert.Multiple(() =>
		{
			Assert.That(resultList, Has.Count.EqualTo(2));
			Assert.That(resultList.First().Name, Is.EqualTo("Tag 1"));
			Assert.That(resultList.Last().Name, Is.EqualTo("Tag 2"));
		});
	}
}