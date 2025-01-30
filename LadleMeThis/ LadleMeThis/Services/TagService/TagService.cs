﻿using LadleMeThis.Models.IngredientsModels;
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

        public async Task AddAsync(TagDTO tagDTO)
        {
            await _tagRepository.AddAsync(new Tag() { Name = tagDTO.Name });
        }

        public async Task DeleteByIdAsync(int tagId)
        {
            await _tagRepository.DeleteByIdAsync(tagId);
        }

        public async Task<IEnumerable<TagDTO>> GetAllAsync()
        {
            var tags = await _tagRepository.GetAllAsync();
            return tags.Select(tag => new TagDTO() { Name = tag.Name }).ToList();
        }

        public async Task<TagDTO?> GetByIdAsync(int tagId)
        {
            var tag = await _tagRepository.GetByIdAsync(tagId);
            return new TagDTO() { Name = tag.Name };
        }

        public async Task<IEnumerable<TagDTO>> GetManyByIdAsync(int[] tagIds)
        {
            throw new NotImplementedException();
        }
    }
}