using LadleMeThis.Models.IngredientsModels;
using LadleMeThis.Models.TagModels;

namespace LadleMeThis.Services.TagService
{
    public interface ITagService
    {
        public Task<IEnumerable<TagDTO>> GetAllAsync();
        public Task<TagDTO?> GetByIdAsync(int tagId);
        public Task<IEnumerable<TagDTO>> GetManyByIdAsync(int[] tagIds);
        public Task AddAsync(TagDTO tagDTO);
        public Task DeleteByIdAsync(int tagId);
    }
}
