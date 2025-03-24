using LadleMeThis.Data.Entity;
using LadleMeThis.Models.IngredientsModels;
using LadleMeThis.Models.TagModels;
using LadleMeThis.Repositories.TagRepository;

namespace LadleMeThis.Services.TagService
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<TagDTO> AddAsync(TagCreateRequest tagCreateRequest)
        {
            var tag = await _tagRepository.AddAsync(new Tag() { Name = tagCreateRequest.Name });
            return new TagDTO() { Name = tag.Name, TagId = tag.TagId };
        }

        public async Task DeleteByIdAsync(int tagId)
        {
            await _tagRepository.DeleteByIdAsync(tagId);
        }

        public async Task<IEnumerable<TagDTO>> GetAllAsync()
        {
            var tags = await _tagRepository.GetAllAsync();
            return tags.Select(tag => new TagDTO() { Name = tag.Name, TagId = tag.TagId }).ToList();
        }

        public async Task<TagDTO> GetByIdAsync(int tagId)
        {
            var tag = await _tagRepository.GetByIdAsync(tagId);
            return new TagDTO() { Name = tag.Name, TagId = tag.TagId };
        }

        public async Task<IEnumerable<TagDTO>> GetManyByIdAsync(int[] tagIds)
        {
            var tags = await _tagRepository.GetManyByIdAsync(tagIds);
            return tags.Select(tag => new TagDTO() { Name = tag.Name, TagId = tag.TagId }).ToList();
        }
    }
}