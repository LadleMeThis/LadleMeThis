using LadleMeThis.Models.TagModels;

namespace LadleMeThis.Repositories.TagRepository;

public interface ITagRepository
{
    public Task<IEnumerable<Tag>> GetAllAsync();
    public Task<Tag?> GetByIdAsync(int tagId);
    public Task<IEnumerable<Tag>> GetManyByIdAsync(int[] tagIds);
    public Task AddAsync(Tag tag);
    public Task DeleteByIdAsync(int tagId);
}