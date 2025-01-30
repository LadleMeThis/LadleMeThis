using LadleMeThis.Models.TagModels;

namespace LadleMeThis.Repositories.TagRepository;

public interface ITagRepository
{
    public Task<IEnumerable<Tag>> GetAllAsync();
    public Task<Tag?> GetByIdAsync(int id);
    public Task AddAsync(Tag tag);
    public Task DeleteAsync(int id);
}